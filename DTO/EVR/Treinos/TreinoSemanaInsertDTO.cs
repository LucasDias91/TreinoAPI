using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.DTO.EVR
{
    public class TreinoSemanaInsertDTO
    {
        public bool Status { get; set; } = true;

        public string Msg { get; set; } = "Novo treino solicitado com sucesso!";

        public DateTime DataInicio { get; set; }
        
        public int IDSemana { get; set; }

    }
}
