using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApresentacoesAPI.DTO.Auth
{
    public class SessoesDTO
    {
        [Key]
        public Int64 IDSessao { get; set; }

        public int IDUsuario { get; set; }

        public string GrantType { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public DateTime datainicio { get; set; }

        public DateTime datafim { get; set; }
    }
}
