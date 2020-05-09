﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TreinoAPI.Db_Context;
using TreinoAPI.DTO.Treinos;

namespace TreinoAPI.DAO
{
    public class TreinosDAO
    {
        TreinoConnectionString DbTreino = new TreinoConnectionString();

        public void PopulaTreinoUsuarios(int IDUsuario)
        {
            DbTreino.Database.ExecuteSqlCommand("Insert Into TreinoUsuarios (IDUsuario,IDSemana, IDDivisao, Executado, Ativo)" +
                                                " Select Distinct @IDUsuario, Semana.IDSemana, Divisao.IDDivisao, null, 0 from Treino$ as Treino" +
                                                " inner Join Semanas as Semana on Semana.IDSemana = Treino.Ciclo" +
                                                " inner join Divisoes as Divisao on Treino.Divisao = Divisao.Divisao",
                                                 new SqlParameter("@IDUsuario", IDUsuario));
        }

        public List<SemanasDTO> SelectSemanas()
        {
            return DbTreino.Semanas.ToList();
        }

        public List<SemanaUsuariosDTO> SelectSemanasUsuarioPorIDUsuario(int IDUsuario)
        {
            return DbTreino.SemanaUsuarios.Where((usuario) => usuario.IDUsuario == IDUsuario).ToList();
        }

        public SemanaDiasDTO SelectSemanaDiaPorIDSemana(int IDSemanaDia)
        {
            return DbTreino.SemanaDias.Where((semana) => semana.IDSemanaDia == IDSemanaDia).FirstOrDefault();
        }

        public List<TreinoUsuariosDTO> SelectTreinoUsuariosPorIDUsuarioAndIDSemana(int IDUsuario, int IDSemana)
        {
            return DbTreino.TreinoUsuarios
                           .Where((item) => item.IDUsuario == IDUsuario && item.IDSemana == IDSemana)
                           .ToList();
        }

        public Object SelectTreino(int IDUsuario)
        {
            return DbTreino.SemanaUsuarios.Where((item) => item.IDUsuario == IDUsuario && item.Ativo == true)
                                          .Select((item)=> new
                                          {
                                              item.IDSemana,
                                              item.DataInicio,
                                              item.DataFim,
                                              Treinos = SelectTreinos(IDUsuario)
                                          }).ToList();

        }

        private Object SelectTreinos(int IDUsuario)
        {
    
            var TreinoUsuarios = DbTreino.TreinoUsuarios.Where((treino) => treino.IDUsuario == IDUsuario && treino.Ativo == true)
                                                       .ToList();

            var Divisoes = DbTreino.Divisoes.Where((divisao) => divisao.Ativo == true)
                                            .ToList();

            var Treinos = DbTreino.Treinos.Where((treino) => treino.Ativo == true)
                                          .ToList();

            var Grupos = DbTreino.Grupos.Where((grupo) => grupo.Ativo == true)
                                        .ToList();

            var SemanaDias = DbTreino.SemanaDias.Where((grupo) => grupo.Ativo == true)
                                                .ToList();


            return (from TreinoUsuario in TreinoUsuarios
                    join SemanaDia in SemanaDias on TreinoUsuario.IDSemanaDia equals SemanaDia.IDSemanaDia
                    join Divisao in Divisoes on TreinoUsuario.IDDivisao equals Divisao.IDDivisao
                    into temp
                    from Divisao in temp.DefaultIfEmpty()
                    select new
                    {
                        SemanaDia.SemanaDia,
                        Divisao.Divisao,
                        QtdExercicios = Treinos.Where((treino) => TreinoUsuario.IDSemana == treino.IDSemana && Divisao.IDDivisao == treino.IDDivisao).Count(),
                        Grupos = (
                        (from Treino in Treinos.Where((treino) => TreinoUsuario.IDSemana == treino.IDSemana && Divisao.IDDivisao == treino.IDDivisao)
                         join Grupo in Grupos on Treino.IDGrupo equals Grupo.IDGrupo
                         into temp2
                         from Grupo in temp2.DefaultIfEmpty()
                         select Grupo.Grupo).Distinct()),
                         Exercicios = SelectExercicios(TreinoUsuario.IDSemana, Divisao.IDDivisao)
                    }).Distinct();
        }

        private Object SelectExercicios(int IDSemana, int IDDivisao)
        {

            var Treinos = DbTreino.Treinos.Where((treino) => treino.IDSemana == IDSemana && treino.IDDivisao == IDDivisao && treino.Ativo == true) ;
            var Exercicios = DbTreino.Exercicios.Where((exercicio) => exercicio.Ativo == true);
            var SxRs = DbTreino.SxRs.Where((sxr) => sxr.Ativo == true);
            var TecAvancadas = DbTreino.TecAvancadas.Where((tec) => tec.Ativo == true);

            return (from Treino in Treinos
                    join SxR in SxRs on Treino.IDSxR equals SxR.IDSxR
                    join TecAvancada in TecAvancadas on Treino.IDTecAvancada equals TecAvancada.IDTecAvancada
                    join Exercicio in Exercicios on Treino.IDExercicio equals Exercicio.IDExercicio
                    into temp
                    from Exercicio in temp.DefaultIfEmpty()
                    select new
                    {
                        Exercicio.Exercicio,
                        SxR.SxR,
                        TecAvancada.TecAvancada

                    }).Distinct();

        }


        public void InsertTreinoSemanas(int IDUsuario, DateTime DataInicio, int IDSemana, int IDSemanaDia)
        {
            try
            {
                SemanaUsuariosDTO _SemanaUsuarioAdd = PrepareSemanaUsuario(IDUsuario, DataInicio, IDSemana);
                List<TreinoUsuariosDTO> _TreinoUsuarios = DbTreino.TreinoUsuarios.Where((item) => item.IDUsuario == IDUsuario && item.IDSemana == IDSemana).ToList();

                int _IDSemanaDia = IDSemanaDia;
                _TreinoUsuarios.ForEach((item) => 
                {
                    item.IDSemanaDia = _IDSemanaDia;
                    item.Ativo = true;
                    if(_IDSemanaDia == 7)
                    {
                        _IDSemanaDia = 1;
                    }
                    else
                    {
                        _IDSemanaDia = _IDSemanaDia + 1;
                    }
                });

                DbTreino.UpdateRange(_TreinoUsuarios);
                DbTreino.Add(_SemanaUsuarioAdd);

            }
            catch
            {
                throw;
            }

            DbTreino.SaveChanges();
        }

        public void UpdateTreinoSemanas(int IDUsuario, DateTime DataInicio,int IDSemana, int IDSemanaNovo, List<TreinoUsuarioEditDTO> TreinoDias)
        {
            try
            {
                SemanaUsuariosDTO _SemanaUsuarioAdd = PrepareSemanaUsuario(IDUsuario, DataInicio, IDSemanaNovo);
                List<TreinoUsuariosDTO> _TreinoUsuariosAntigo = DbTreino.TreinoUsuarios.Where((item) => item.IDUsuario == IDUsuario && item.IDSemana == IDSemana).ToList();
                _TreinoUsuariosAntigo.ForEach((item) => 
                {
                    item.Executado = TreinoDias.Where((treino) => treino.IDSemanaDia == item.IDSemanaDia).FirstOrDefault().Executado;
                    item.Ativo = false;
                });

                SemanaUsuariosDTO _SemanaUsuario = DbTreino.SemanaUsuarios.Where((semana) => semana.IDUsuario == IDUsuario && semana.IDSemana == IDSemana).FirstOrDefault();
                _SemanaUsuario.Ativo = false;

                List<TreinoUsuariosDTO> _TreinoUsuariosNovo = DbTreino.TreinoUsuarios.Where((item) => item.IDUsuario == IDUsuario && item.IDSemana == IDSemanaNovo).ToList();
                _TreinoUsuariosNovo.ForEach((item) => item.Ativo = true);

                DbTreino.Update(_SemanaUsuario);
                DbTreino.UpdateRange(_TreinoUsuariosAntigo);
                DbTreino.UpdateRange(_TreinoUsuariosNovo);
                DbTreino.Add(_SemanaUsuarioAdd);

            }
            catch
            {
                throw;
            }

            DbTreino.SaveChanges();
        }

        public void UpdateTreino(int IDUsuario, DateTime DataInicio, int IDSemana)
        {
            try
            {
                SemanaUsuariosDTO _SemanaUsuarioAdd = PrepareSemanaUsuario(IDUsuario, DataInicio, IDSemana);
                DbTreino.Add(_SemanaUsuarioAdd);

            }
            catch
            {
                throw;
            }

            DbTreino.SaveChanges();
        }

        private SemanaUsuariosDTO PrepareSemanaUsuario(int IDUsuario, DateTime DataInicio, int IDSemana)
        {
            SemanaUsuariosDTO _SemanaUsuarioAdd = new SemanaUsuariosDTO();
            List<TreinoUsuariosDTO> _treinoUsuarios = SelectTreinoUsuariosIDSemana(IDSemana);
            _SemanaUsuarioAdd.IDUsuario = IDUsuario;
            _SemanaUsuarioAdd.DataInicio = DataInicio;
            _SemanaUsuarioAdd.DataFim = DataInicio.AddDays(_treinoUsuarios.Count());
            _SemanaUsuarioAdd.IDSemana = IDSemana;
            return _SemanaUsuarioAdd;
        }

        private List<TreinoUsuariosDTO> SelectTreinoUsuariosIDSemana(int IDSemana)
        {
            return DbTreino.TreinoUsuarios.Where((treino) => treino.IDSemana == IDSemana).ToList();
        }

        private List<SemanaUsuariosDTO> SelectSemanaUsuariosPorIDUsuario(int IDUsuario)
        {
            return DbTreino.SemanaUsuarios.Where((treino) => treino.IDUsuario == IDUsuario).ToList();
        }

    }
}
