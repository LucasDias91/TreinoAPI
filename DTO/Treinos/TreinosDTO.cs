using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    [Table("Treinos")]
    public class TreinosDTO
    {
        [Key]
        public int IDTreino { get; set; }

        public int IDSemana { get; set; }

        public int IDTipo { get; set; }

        public int IDDivisao { get; set; }

        public int IDGrupo { get; set; }

        public int IDExercicio { get; set; }

        public  int IDSxR { get; set; }

        public  Nullable<int> IDTecAvancada { get; set; }

        public int IDIntervalo { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastEditDate { get; set; }

        public bool Ativo { get; set; }
    }
}
