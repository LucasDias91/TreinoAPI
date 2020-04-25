using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApresentacoesAPI.DTO.Auth
{
    public class RefreshTokenDataDTO
    {
        public string RefreshToken { get; set; }

        public string IDUsuario { get; set; }

    }
}
