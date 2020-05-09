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

            List<SemanaUsuariosDTO> _SemanaUsuario = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(IDUsuario);

            if (_SemanaUsuario.Count() > 0)
            {
                _TreinoSemanaInsert.Status = false;
                _TreinoSemanaInsert.Msg = "Ciclo de treino já foi iniciado!";
                return _TreinoSemanaInsert;
            }

            if (DataInicio < DateTime.Now)
            {
                _TreinoSemanaInsert.Status = false;
                _TreinoSemanaInsert.Msg = "Data Inicio deve ser maior ou igual a data de hoje!";
                return _TreinoSemanaInsert;
            }

            List<SemanasDTO> _Semanas = TreinosDAO.SelectSemanas();
            List<SemanaUsuariosDTO> _SemanasUsuarios = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(IDUsuario);
            var Semanas = _Semanas.Where(item => !_SemanasUsuarios.Any(item2 => item2.IDSemana == item.IDSemana));
            int _IDSemana = Semanas.Min((semana) => semana.IDSemana);

            _TreinoSemanaInsert.DataInicio = DataInicio;
            _TreinoSemanaInsert.IDSemana = _IDSemana;
            return _TreinoSemanaInsert;
        }

        public static TreinoSemanaUpdateDTO UpdateTreinoSemanaEVR(int IDUsuario, TreinoSemanaEditDTO TreinoSemanaEdit, TreinosDAO TreinosDAO)
        {
            TreinoSemanaUpdateDTO _TreinoSemanaUpdate = new TreinoSemanaUpdateDTO();

            List<SemanaUsuariosDTO> _SemanaUsuarios = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(IDUsuario);

            SemanaDiasDTO _SemanaDias = TreinosDAO.SelectSemanaDiaPorIDSemana(TreinoSemanaEdit.IDSemanaDia);

            if (_SemanaDias == null)
            {
                _TreinoSemanaUpdate.Status = false;
                _TreinoSemanaUpdate.Msg = "Dia da semana inválido!";
                return _TreinoSemanaUpdate;
            }

            if (_SemanaUsuarios.Count() == 0)
            {
                _TreinoSemanaUpdate.Status = false;
                _TreinoSemanaUpdate.Msg = "Ciclo de treino ainda não foi iniciado!";
                return _TreinoSemanaUpdate;

            }

            DateHelpers _DateHelpers = new DateHelpers();

            DateTime DataInicio = _DateHelpers.GetNextDateForDay(TreinoSemanaEdit.IDSemanaDia);

            if (DataInicio < DateTime.Now)
            {
                _TreinoSemanaUpdate.Status = false;
                _TreinoSemanaUpdate.Msg = "Data Inicio deve ser maior ou igual a data de hoje";
                return _TreinoSemanaUpdate;
            }

            SemanaUsuariosDTO _SemanaUsuario = _SemanaUsuarios.Where((semana) => semana.IDUsuario == IDUsuario && semana.IDSemana == TreinoSemanaEdit.IDSemana).FirstOrDefault();

            if (_SemanaUsuario == null)
            {
                _TreinoSemanaUpdate.Status = false;
                _TreinoSemanaUpdate.Msg = "Semana de treino não encontrada!";
                return _TreinoSemanaUpdate;
            }

            List<TreinoUsuariosDTO> _TreinoUsuario = TreinosDAO.SelectTreinoUsuariosPorIDUsuarioAndIDSemana(IDUsuario, TreinoSemanaEdit.IDSemana);
            var SemanaDiasNSelecionadas = _TreinoUsuario.Where(item => !TreinoSemanaEdit.TreinoUsuarioEdit.Any(item2 => item2.IDSemanaDia == item.IDSemanaDia));

            if (SemanaDiasNSelecionadas.Count() > 0)
            {
                _TreinoSemanaUpdate.Status = false;
                _TreinoSemanaUpdate.Msg = "Voçê não enviou todos os dias da semana";
                return _TreinoSemanaUpdate;
            }

            if (_SemanaUsuario.DataFim > DateTime.Now)
            {
                _TreinoSemanaUpdate.Status = false;
                _TreinoSemanaUpdate.Msg = "Voce só poderá solicitar um novo treino a partir de: " + _SemanaUsuario.DataFim;
                 return _TreinoSemanaUpdate;
            }

            List<SemanasDTO> _Semanas = TreinosDAO.SelectSemanas();
            List<SemanaUsuariosDTO> _SemanasUsuarios = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(IDUsuario);
            var SemanasNSelecionadas = _Semanas.Where(item => !_SemanasUsuarios.Any(item2 => item2.IDSemana == item.IDSemana));
            int _IDSemanaNovo = SemanasNSelecionadas.Min((semana) => semana.IDSemana);

            _TreinoSemanaUpdate.DataInicio = DataInicio;
            _TreinoSemanaUpdate.IDSemanaNovo = _IDSemanaNovo;

            return _TreinoSemanaUpdate;
        }
    }
}
