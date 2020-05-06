using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    [Table("SemanaUsuarios")]
    public class SemanaUsuariosDTO
    {
        [Key]
        public int IDSemanaUsuario { get; set; }

        public int IDUsuario { get; set; }

        public int IDSemana { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }
    }
}
