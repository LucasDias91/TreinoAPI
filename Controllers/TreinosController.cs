using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TreinoAPI.Claims;
using TreinoAPI.DAO;
using TreinoAPI.DTO.Treinos;

namespace TreinoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinosController : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("Treino/Semana")]
        public IActionResult PostTreino([FromBody] TreinoAddDTO TreinoAdd,
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

                List<SemanaUsuariosDTO> _SemanaUsuario = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(_IDUsuario);

                if(_SemanaUsuario.Count() == 0)
                {
                    if(TreinoAdd.DataInicio < DateTime.Now)
                    {
                        return BadRequest("Data Inicio deve ser maior ou igual a data de hoje");
                    }
                    List<SemanasDTO> _Semanas = TreinosDAO.SelectSemanas();
                    List<SemanaUsuariosDTO> _SemanasUsuarios = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(_IDUsuario);
                    var Semanas = _Semanas.Where(item => !_SemanasUsuarios.Any(item2 => item2.IDSemana == item.IDSemana));
                    int _IDSemana = Semanas.Min((semana) => semana.IDSemana);
                    TreinosDAO.InsertTreinoSemanas(_IDUsuario, TreinoAdd.DataInicio, _IDSemana);
                }

            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException);
            }

            return Ok();
        }

        [HttpPut]
        [Route("Treino/Semana")]
        [AllowAnonymous]
        public IActionResult PutTreino([FromBody] TreinoEditDTO TreinoAdd,
                                       [FromServices] TreinosDAO TreinosDAO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int _IDUsuario = User.Identity.GetIDUsuario();

                List<SemanaUsuariosDTO> _SemanaUsuario = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(_IDUsuario);

                if (_SemanaUsuario.Count() > 0)
                {
                    if (TreinoAdd.DataInicio < DateTime.Now)
                    {
                        return BadRequest("Data Inicio deve ser maior ou igual a data de hoje");
                    }
                    List<SemanasDTO> _Semanas = TreinosDAO.SelectSemanas();
                    List<SemanaUsuariosDTO> _SemanasUsuarios = TreinosDAO.SelectSemanasUsuarioPorIDUsuario(_IDUsuario);
                    var Semanas = _Semanas.Where(item => !_SemanasUsuarios.Any(item2 => item2.IDSemana == item.IDSemana));
                    int _IDSemana = Semanas.Min((semana) => semana.IDSemana);
                    //TreinosDAO.InsertTreino(_IDUsuario, TreinoAdd.DataInicio, _IDSemana);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

    }
}