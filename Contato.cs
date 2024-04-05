using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace av2
{
    internal class Contato
    {
        public int? idContato {  get; set; }
        public string? nome { get; set; }
        public int? telefone { get; set; }
        public string? email { get; set; }
        public int[]? fkidgrupo { get; set; }
        public bool ativo { get; set; }
    }
}
