using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.Treinos
{
    [Table("Exercicios")]
    public class ExerciciosDTO
    {
        [Key]
        public int IDExercicio { get; set; }

        public string Exercicio { get; set; }
    }
}
