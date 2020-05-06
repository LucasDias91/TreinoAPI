using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    [Table("Ciclos")]
    public class CiclosDTO
    {
        [Key]
        public int IDCiclo { get; set; }

        public string Ciclo { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
