using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TreinoAPI.Claims;
using TreinoAPI.DAO;
using TreinoAPI.DTO.Treinos;
using TreinoAPI.Helpers;

namespace TreinoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinosController : ControllerBase
    {
        [HttpPost]
        [Route("Treino/Semana")]
        [AllowAnonymous]
        public IActionResult PostTreinoSemana([FromBody] TreinoSemanaAddDTO TreinoSemanaAdd,
                                              [FromServices] TreinosDAO TreinosDAO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //int _IDUsuario = User.Identity.GetIDUsuario();
                int _IDUsuario = 1;

                SemanaDiasDTO _SemanaDias = TreinosDAO.SelectSemanaDiaPorIDSemana(TreinoSemanaAdd.IDSemanaDia);

                if(_SemanaDias == null)
                {
                    return BadRequest("Dia da semana inválido!");
                }

                DateHelpers _DateHelpers = new DateHelpers();

                DateTime DataInicio = _DateHelpers.GetNextDateForDay(TreinoSemanaAdd.IDSemanaDia);

                List<SemanaUsuariosDTO> _SemanaUsuario = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(_IDUsuario);

                if (_SemanaUsuario.Count() > 0)
                {
                    return BadRequest("Ciclo de treino já foi iniciado!");
                }

                if (DataInicio < DateTime.Now)
                {
                    return BadRequest("Data Inicio deve ser maior ou igual a data de hoje!");
                }

                  List<SemanasDTO> _Semanas = TreinosDAO.SelectSemanas();
                  List<SemanaUsuariosDTO> _SemanasUsuarios = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(_IDUsuario);
                  var Semanas = _Semanas.Where(item => !_SemanasUsuarios.Any(item2 => item2.IDSemana == item.IDSemana));
                  int _IDSemana = Semanas.Min((semana) => semana.IDSemana);
                  TreinosDAO.InsertTreinoSemanas(_IDUsuario, DataInicio, _IDSemana, TreinoSemanaAdd.IDSemanaDia);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException);
            }

            return Ok("Novo treino solicitado com sucesso!");
        }

        [HttpPut]
        [Route("Treino/Semana")]
        [AllowAnonymous]
        public IActionResult PutTreinoSemana([FromBody] TreinoSemanaEditDTO TreinoSemanaEdit,
                                             [FromServices] TreinosDAO TreinosDAO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //int _IDUsuario = User.Identity.GetIDUsuario();
                int _IDUsuario = 1;

                List<SemanaUsuariosDTO> _SemanaUsuarios = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(_IDUsuario);

                SemanaDiasDTO _SemanaDias = TreinosDAO.SelectSemanaDiaPorIDSemana(TreinoSemanaEdit.IDSemanaDia);

                if (_SemanaDias == null)
                {
                    return BadRequest("Dia da semana inválido!");
                }

                if (_SemanaUsuarios.Count() == 0)
                {
                    return BadRequest("Ciclo de treino ainda não foi iniciado!");
                }

                DateHelpers _DateHelpers = new DateHelpers();

                DateTime DataInicio = _DateHelpers.GetNextDateForDay(TreinoSemanaEdit.IDSemanaDia);

                if (DataInicio < DateTime.Now)
                {
                    return BadRequest("Data Inicio deve ser maior ou igual a data de hoje");
                }

                SemanaUsuariosDTO _SemanaUsuario = _SemanaUsuarios.Where((semana) => semana.IDUsuario == _IDUsuario && semana.IDSemana == TreinoSemanaEdit.IDSemana).FirstOrDefault();

                if (_SemanaUsuario == null)
                {
                    return BadRequest("Semana de treino não encontrada!");
                }

                List<TreinoUsuariosDTO> _TreinoUsuario = TreinosDAO.SelectTreinoUsuariosPorIDUsuarioAndIDSemana(_IDUsuario, TreinoSemanaEdit.IDSemana);
                var SemanaDiasNSelecionadas = _TreinoUsuario.Where(item => !TreinoSemanaEdit.TreinoUsuarioEdit.Any(item2 => item2.IDSemanaDia == item.IDSemanaDia));

                if(SemanaDiasNSelecionadas.Count() > 0)
                {
                    return BadRequest("Voçê não enviou todos os dias da semana");
                }

                if (_SemanaUsuario.DataFim > DateTime.Now)
                {
                    return BadRequest("Voce só poderá solicitar um novo treino a partir de: " + _SemanaUsuario.DataFim);
                }

                List<SemanasDTO> _Semanas = TreinosDAO.SelectSemanas();
                List<SemanaUsuariosDTO> _SemanasUsuarios = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(_IDUsuario);
                var SemanasNSelecionadas = _Semanas.Where(item => !_SemanasUsuarios.Any(item2 => item2.IDSemana == item.IDSemana));
                int _IDSemanaNovo = SemanasNSelecionadas.Min((semana) => semana.IDSemana);
                TreinosDAO.UpdateTreinoSemanas(_IDUsuario, DataInicio, TreinoSemanaEdit.IDSemana, _IDSemanaNovo, TreinoSemanaEdit.TreinoUsuarioEdit);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok("Novo treino solicitado com sucesso!");
        }

    }
}