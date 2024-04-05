using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace av2
{
    internal class Grupo
    {
        public int? idGrupo {  get; set; }
        public string? nomeGrupo { get; set; }

        public int[]? fkidusuario { get; set; }

        public bool ativo {  get; set; }


    }
}
