using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreinoAPI.DTO.Helpers;

namespace TreinoAPI.Helpers
{
    public class ResultadoHelper
    {
        public ResultadoDTO PreparaResultado(Object Dados)
        {
            ResultadoDTO _Resultado = new ResultadoDTO();
            _Resultado.DataAtualizacao = DateTime.Now;
            _Resultado.Dados = Dados;
            return _Resultado;
        }
    }
}
