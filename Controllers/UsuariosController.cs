using System;
using Microsoft.AspNetCore.Mvc;
using TreinoAPI.Claims;
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
            int _IDUsuario = User.Identity.GetIDUsuario();

            try
            {
                _Usuarios = _UsuariosDAO.SelectUsuario(_IDUsuario);

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