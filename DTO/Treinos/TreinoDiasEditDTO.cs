using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    public class TreinoDiasEditDTO
    {
        [Required]
        public Nullable<int> IDTreinoDia { get; set; }

        [Required]
        public Nullable<bool> Executado { get; set; }
    }
}
