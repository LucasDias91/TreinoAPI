using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TreinoAPI.Claims;
using TreinoAPI.DAO;
using TreinoAPI.DTO.Helpers;
using TreinoAPI.DTO.Usuarios;
using TreinoAPI.Helpers;

namespace TreinoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        #region getUsuario
        [HttpGet]
        [Route("Display")]
        [AllowAnonymous]
        public IActionResult GetUsuarioDisplay([FromServices] UsuariosDAO _UsuariosDAO,
                                               [FromQuery] ParamsDTO Params)
        {

            ResultadoHelper _ResultadoHelper = new ResultadoHelper();
            ResultadoDTO _Resultado = new ResultadoDTO();
            // int _IDUsuario = User.Identity.GetIDUsuario();
            int _IDUsuario = 1;

            try
            {
               
               UsuarioDisplayDTO _UsuarioDisplay = _UsuariosDAO.SelectUsuarioDisplay(_IDUsuario, Params.DataAtualizacao);
                _Resultado = _ResultadoHelper.PreparaResultado(_UsuarioDisplay);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(_Resultado);
        }
        #endregion
    }
}