using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace av2
{
    public class Contato
    {
        private int _IDContato;
        public int IDContato
        {
            get {
                return _IDContato;
            }
            private set
            {
                _IDContato = value;
            }

        }

        private string _Nome;
        public string Nome {
            get
            {
                return _Nome.ToUpper();
            }
            private set
            { 
                _Nome = value;
            }
        }

        private long _Telefone;
        public long Telefone {
            get
            {
                return _Telefone;
            }
            private set
            {
                _Telefone = value;
            }
        }

        private string _Email;
        public string Email {
            get
            {
                return _Email;
            }
            private set
            {
                _Email = value;
            }

        }

        private int[] _IDGrupo;
        public int[] IDGrupo {
            get
            {
                return _IDGrupo;
            }
            private set
            {
                _IDGrupo = value;
            }
        }

        public void CriarContato() { 
            
        }
    }
}
