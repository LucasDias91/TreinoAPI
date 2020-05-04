using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    [Table("Ciclos")]
    public class CiclosDTO
    {
        public int IDCiclo { get; set; }

        public bool Ativo { get; set; }
    }
}
