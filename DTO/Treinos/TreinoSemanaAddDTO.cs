using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    public class TreinoSemanaAddDTO
    {
        [Required]
        public int IDSemanaDia { get; set; }
    }
}
