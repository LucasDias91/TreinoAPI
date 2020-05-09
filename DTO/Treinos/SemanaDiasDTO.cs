using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    [Table("SemanaDias")]
    public class SemanaDiasDTO
    {
        [Key]
        public int IDSemanaDia { get; set; }

        public string SemanaDia { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastEditDate { get; set; }

        public bool Ativo { get; set; }
    }
}
