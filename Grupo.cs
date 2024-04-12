using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace av2
{
    internal class Grupo
    {
        private int _IDGrupo;
        public int IDGrupo {
            get
            {
                return _IDGrupo;
            }
            private set
            {
                _IDGrupo = value;
            }
        }

        private string _NomeGrupo;
        public string NomeGrupo {
            get
            {
                return _NomeGrupo;
            }
            private set
            {
                _NomeGrupo = value;
            }
        }

        private int[] _IDContato;
        public int[] IDContato
        {
            get
            {
                return _IDContato;
            }
            private set
            {
                _IDContato = value;
            }
        }

    }
}
