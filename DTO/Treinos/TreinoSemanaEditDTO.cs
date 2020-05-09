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
        public int IDSemanaDia { get; set; }

        [Required]
        public int IDSemana { get; set; }


        [Required]
        public List<TreinoUsuarioEditDTO> TreinoUsuarioEdit { get; set; }
 
    }
}
