using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TreinoAPI.Helpers
{
    public class ParamsDTO
    {
        [DefaultValue("1991-09-08")]
        public Nullable<DateTime> DataAtualizacao { get; set; }
    }
}
