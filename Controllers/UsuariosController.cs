using System;
using System.Collections.Generic;
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
        public IActionResult GetUsuarioDisplay([FromServices] UsuariosDAO _UsuariosDAO,
                                               [FromQuery] ParamsDTO Params)
        {

            ResultadoDTO _Resultado = new ResultadoDTO();
            int _IDUsuario = User.Identity.GetIDUsuario();

            try
            {
               
               List<UsuarioDisplayDTO> _UsuarioDisplay = _UsuariosDAO.SelectUsuarioDisplay(_IDUsuario, Params.DataAtualizacao);
                _Resultado = ResultadoHelper.PreparaResultado(_UsuarioDisplay);
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