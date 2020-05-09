using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.EVR.Treinos
{
    public class TreinoSemanaUpdateDTO
    {
        public bool Status { get; set; } = true;

        public string Msg { get; set; } = "Novo treino solicitado com sucesso!";

        public DateTime DataInicio { get; set; }

        public int IDTipo { get; set; }

        public int IDSemanaNovo { get; set; }

        public int IDSemanaDia { get; set; }
    }
}
