using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApresentacoesAPI.DTO.Auth
{

    public class LoginDTO
    {
        [Required]
        public string Grant_Type { get; set; }

      //  [RequiredIf("Grant_Type== 'password'", ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

    //    [RequiredIf("Grant_Type=='password'", ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        public string SecretKeyCliente { get; set; }

        public string SecretKeyUsuario { get; set; }

       // [RequiredIf("Grant_Type== 'refresh_token'", ErrorMessage = "RefreshToken is required.")]
        public string RefreshToken { get; set; }
    }
}
