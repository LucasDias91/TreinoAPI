using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreinoAPI.DTO.Treinos;

namespace TreinoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinosController : ControllerBase
    {
        [HttpPost]
        [Route("Treino")]
        [AllowAnonymous]
        public IActionResult PostTreino([FromBody] TreinoAddDTO TreinoAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(TreinoAdd);

        }

    }
}