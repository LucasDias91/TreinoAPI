using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    public class TreinoSemanaEditDTO
    {
        [Required]
        public int IDTreinoUsuario { get; set; }

        [Required]
        public bool Executado { get; set; }

        [Required]
        public int TempoTreino { get; set; }

        [Required]
        public DateTime DataExecucao { get; set; }

        
    }
}
