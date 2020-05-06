using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    public class TreinoAddDTO
    {
        [Required]
        public DateTime DataSolicatacao { get; set; }
    }
}
