using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Av2
{
    internal class EntidadeJSON<T>
    {
        public List<T> Dados { get; set; } = new List<T>();
        public int UltimoAdicionado { get; set; }

    }
}
