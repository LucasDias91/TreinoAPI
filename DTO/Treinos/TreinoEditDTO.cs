using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    public class TreinoEditDTO
    {
        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public List<TreinoDiasEditDTO> TreinoDias { get; set; }
 
    }
}
