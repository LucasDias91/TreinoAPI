using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    [Table("TecAvancadas")]
    public class TecAvancadasDTO
    {
        [Key]
        public int IDTecAvancada { get; set; }

        public string TecAvancada { get; set; }

        public Nullable<DateTime> CreationDate { get; set; }

        public Nullable<DateTime> LastEditDate { get; set; }

        public bool Ativo { get; set; }
    }
}
