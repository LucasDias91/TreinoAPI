using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Usuarios
{
    [Table("Usuarios")]
    public class UsuariosDTO
    {
        [Key]
        public int IDUsuario { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Cep { get; set; }

        public string Endereco { get; set; }

        public string Numero { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string UF { get; set; }

        public string Foto64 { get; set; }

        public bool Pendente { get; set; }

        public bool Ativo { get; set; } = true;

    }
}
