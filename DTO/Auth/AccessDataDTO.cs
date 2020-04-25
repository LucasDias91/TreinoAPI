using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApresentacoesAPI.DTO.Auth
{
    public class AccessDataDTO
    {
        public string secretKeyCliente { get; set; }

        public string secretKeyUsuario { get; set; }

        public string created { get; set; }

        public string expiration { get; set; }

        public string refreshToken { get; set; }

        public string accessToken { get; set; }

        public string idCiclo { get; set; }
    }
}
