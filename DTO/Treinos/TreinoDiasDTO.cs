using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    [Table("TreinoDia")]
    public class TreinoDiasDTO
    {
        [Key]
        public int IDTreinoDia { get; set; }

        public int IDUsuario { get; set; }

        public int IDCiclo { get; set; }

        public int IDDivisao { get; set; }

        public bool Executado { get; set; }

        public bool Ativo { get; set; }

        public Nullable<DateTime> CreationDate { get; set; }

        public Nullable<DateTime> LastEditDate { get; set; }
    }
}
