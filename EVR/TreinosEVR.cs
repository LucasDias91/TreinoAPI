using System;
using System.Collections.Generic;
using System.Linq;
using TreinoAPI.DAO;
using TreinoAPI.DTO.EVR;
using TreinoAPI.DTO.EVR.Treinos;
using TreinoAPI.DTO.Treinos;
using TreinoAPI.Helpers;

namespace TreinoAPI.EVR
{
    public static class TreinosEVR
    {
        public static TreinoSemanaInsertDTO InsertTreinoSemanaEVR(int IDUsuario,TreinoSemanaAddDTO TreinoSemanaAdd, TreinosDAO TreinosDAO)
        {
            TreinoSemanaInsertDTO _TreinoSemanaInsert = new TreinoSemanaInsertDTO();

            SemanaDiasDTO _SemanaDias = TreinosDAO.SelectSemanaDiaPorIDSemana(TreinoSemanaAdd.IDSemanaDia);

            if (_SemanaDias == null)
            {
                _TreinoSemanaInsert.Status = false;
                _TreinoSemanaInsert.Msg = "Dia da semana inválido!";
                return _TreinoSemanaInsert;
            }

            DateHelpers _DateHelpers = new DateHelpers();

            DateTime DataInicio = _DateHelpers.GetNextDateForDay(TreinoSemanaAdd.IDSemanaDia);

            List<SemanaUsuariosDTO> _SemanaUsuarios = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(IDUsuario);

            if (DataInicio < DateTime.Now)
            {
                _TreinoSemanaInsert.Status = false;
                _TreinoSemanaInsert.Msg = "Data Inicio deve ser maior ou igual a data de hoje!";
                return _TreinoSemanaInsert;
            }

            SemanaUsuariosDTO _SemanaUsuario = _SemanaUsuarios.Where((semana) => semana.IDUsuario == IDUsuario && semana.Ativo == true).FirstOrDefault();

            if(_SemanaUsuario != null)
            {
                if (_SemanaUsuario.DataFim > DateTime.Now)
                {
                    _TreinoSemanaInsert.Status = false;
                    _TreinoSemanaInsert.Msg = "Voce só poderá solicitar um novo treino a partir de: " + _SemanaUsuario.DataFim;
                    return _TreinoSemanaInsert;
                }
            }

            List<SemanasDTO> _Semanas = TreinosDAO.SelectSemanas();
            List<SemanaUsuariosDTO> _SemanasUsuarios = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(IDUsuario);
            var Semanas = _Semanas.Where(item => !_SemanasUsuarios.Any(item2 => item2.IDSemana == item.IDSemana));
            int _IDSemana = Semanas.Min((semana) => semana.IDSemana);
            int _IDTipo = TreinosDAO.SelectTreinosPorIDSemana(_IDSemana).FirstOrDefault().IDTipo;

            _TreinoSemanaInsert.IDTipo = _IDTipo;
            _TreinoSemanaInsert.DataInicio = DataInicio;
            _TreinoSemanaInsert.IDSemana = _IDSemana;
            return _TreinoSemanaInsert;
        }

        public static TreinoSemanaUpdateDTO UpdateTreinoSemanaEVR(int IDUsuario, TreinoSemanaEditDTO TreinoSemanaEdit, TreinosDAO TreinosDAO)
        {
            TreinoSemanaUpdateDTO _TreinoSemanaUpdate = new TreinoSemanaUpdateDTO();

            TreinoUsuariosDTO _TreinoUsuarios = TreinosDAO.SelectTreinoUsuariosPorID(IDUsuario, TreinoSemanaEdit.IDTreinoUsuario);

            if (_TreinoUsuarios == null)
            {
                _TreinoSemanaUpdate.Status = false;
                _TreinoSemanaUpdate.Msg = "Treino não existe!";
                return _TreinoSemanaUpdate;
            }

            if (_TreinoUsuarios != null)
            {
                if(_TreinoUsuarios.DataExecucao != null)
                {
                    _TreinoSemanaUpdate.Status = false;
                    _TreinoSemanaUpdate.Msg = "O treino já foi salvo em " + _TreinoUsuarios.DataExecucao;
                    return _TreinoSemanaUpdate;
                }
            }

            return _TreinoSemanaUpdate;
        }
    }
}
