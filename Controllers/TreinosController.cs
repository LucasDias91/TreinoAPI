using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TreinoAPI.Claims;
using TreinoAPI.DAO;
using TreinoAPI.DTO.EVR;
using TreinoAPI.DTO.EVR.Treinos;
using TreinoAPI.DTO.Helpers;
using TreinoAPI.DTO.Treinos;
using TreinoAPI.EVR;
using TreinoAPI.Helpers;

namespace TreinoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinosController : ControllerBase
    {

        [HttpGet]
        [Route("Treino/Semana")]
        public IActionResult GetTreino([FromQuery] ParamsDTO Params,
                                              [FromServices] TreinosDAO TreinosDAO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Object _Result = new Object();
            ResultadoDTO _Resultado = new ResultadoDTO();

            try
            {
                 int _IDUsuario = User.Identity.GetIDUsuario();
                _Result = TreinosDAO.SelectTreino(_IDUsuario);
                _Resultado = ResultadoHelper.PreparaResultado(_Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }

            return Ok(_Resultado);
        }

        [HttpPost]
        [Route("Treino/Semana")]
        public IActionResult PostTreinoSemana([FromBody] TreinoSemanaAddDTO TreinoSemanaAdd,
                                              [FromServices] TreinosDAO TreinosDAO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TreinoSemanaInsertDTO TreinoSemanaInsert = new TreinoSemanaInsertDTO();

            try
            {
                int _IDUsuario = User.Identity.GetIDUsuario();

                TreinoSemanaInsert = TreinosEVR.InsertTreinoSemanaEVR(_IDUsuario, TreinoSemanaAdd, TreinosDAO);

                if (!TreinoSemanaInsert.Status)
                {
                    return BadRequest(TreinoSemanaInsert.Msg);
                }

                TreinosDAO.InsertTreinoSemanas(_IDUsuario, TreinoSemanaInsert.DataInicio, TreinoSemanaInsert.IDSemana, TreinoSemanaAdd.IDSemanaDia, TreinoSemanaInsert.IDTipo);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException);
            }

            return Ok(TreinoSemanaInsert.Msg);
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

            TreinoSemanaUpdateDTO TreinoSemanaUpdate = new TreinoSemanaUpdateDTO();

            try
            {
                int _IDUsuario = User.Identity.GetIDUsuario();

                TreinoSemanaUpdate = TreinosEVR.UpdateTreinoSemanaEVR(_IDUsuario, TreinoSemanaEdit, TreinosDAO);

                if (!TreinoSemanaUpdate.Status)
                {
                    return BadRequest(TreinoSemanaUpdate.Msg);
                }

                TreinosDAO.UpdateTreinoSemanas(_IDUsuario, TreinoSemanaUpdate.DataInicio, TreinoSemanaEdit.IDSemana, TreinoSemanaUpdate.IDSemanaNovo, TreinoSemanaEdit.TreinoUsuarioEdit, TreinoSemanaUpdate.IDTipo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(TreinoSemanaUpdate.Msg);
        }

    }
}