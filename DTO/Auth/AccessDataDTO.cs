using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApresentacoesAPI.DTO.Auth
{
    public class AccessDataDTO
    {

        public string Created { get; set; }

        public string Expiration { get; set; }

        public string RefreshToken { get; set; }

        public string AccessToken { get; set; }

    }
}
