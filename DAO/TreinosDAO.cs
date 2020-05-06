using Microsoft.EntityFrameworkCore;
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

        public void PopulaTreinoDias(int IDUsuario)
        {
            DbTreino.Database.ExecuteSqlCommand("Insert Into TreinoDias (IDUsuario,IDSemana, IDDivisao, Executado, Ativo)" +
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

        public void InsertTreino(int IDUsuario, DateTime DataInicio, int IDSemana)
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
            List<TreinoDiasDTO> _treinosDias = SelectTreinoDiasPorIDSemana(IDSemana);
            _SemanaUsuarioAdd.IDUsuario = IDUsuario;
            _SemanaUsuarioAdd.DataInicio = DataInicio;
            _SemanaUsuarioAdd.DataFim = DataInicio.AddDays(_treinosDias.Count());
            _SemanaUsuarioAdd.IDSemana = IDSemana;
            return _SemanaUsuarioAdd;
        }

        private List<TreinoDiasDTO> SelectTreinoDiasPorIDSemana(int IDSemana)
        {
            return DbTreino.TreinoDias.Where((treino) => treino.IDSemana == IDSemana).ToList();
        }

        private List<SemanaUsuariosDTO> SelectSemanaUsuariosPorIDUsuario(int IDUsuario)
        {
            return DbTreino.SemanaUsuarios.Where((treino) => treino.IDUsuario == IDUsuario).ToList();
        }

    }
}
