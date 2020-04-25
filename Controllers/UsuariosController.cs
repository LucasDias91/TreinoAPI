using System;
using Microsoft.AspNetCore.Mvc;
using TreinoAPI.DAO;
using TreinoAPI.DTO.Usuarios;

namespace TreinoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        #region getUsuario
        [HttpGet]
        public IActionResult getUsuario([FromServices] UsuariosDAO _UsuariosDAO)
        {
            UsuariosDTO _Usuarios = new UsuariosDTO();

            try
            {
                _Usuarios = _UsuariosDAO.SelectUsuario(1);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(_Usuarios);
        }
        #endregion
    }
}