using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Av2
{
    public class Contato
    {
        private int _ID;
        private string ?_Nome;
        private string ?_Telefone;
        private string ?_Email;
        private bool _Ativo;
        private List<int> _IDGrupo;


        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
            }

        }

        public bool Ativo
        {
            get { return _Ativo; }
            set
            {
                _Ativo = value;
            }
        }

        public List<int> IDGrupo
        {
            get
            { return _IDGrupo; }
        }

        public string Nome
        {
            get { return _Nome.ToUpper(); }
            set
            {
                if (value.Length >= 3 && value.Length <= 200)
                {
                    _Nome = value;
                }
                else
                {
                    throw new ArgumentException("O nome precisa ter entre 3 e 200 caracteres.");
                }
            }
        }

        public string Telefone
        {
            get { return _Telefone; }
            set
            {
                if (value.Length >= 8 && value.Length <= 13)
                {
                    _Telefone = value;
                }
                else
                {
                    throw new ArgumentException("O telefone precisa ter entre 8 e 13 caracteres.");
                }
            }
        }

        public string Email
        {
            get { return _Email; }
            set
            {
                if (value.Length >= 10 && value.Length <= 50)
                {
                    _Email = value;
                }
                else
                {
                    throw new ArgumentException("O e-mail precisa ter entre 10 e 50 caracteres.");
                }
            }
        }


        public Contato(string nome, string telefone, string email)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            _IDGrupo = new List<int>();
            Ativo = true;
        }

        // Método para adicionar um ID de grupo
        public void AddGrupo(int idGrupo)
        {
            _IDGrupo.Add(idGrupo);
        }

        // Método para remover um ID de grupo
        public void RemoverGrupo(int idGrupo)
        {
            _IDGrupo.Remove(idGrupo);
        }

        public void Exibir()
        {
            Console.WriteLine($"\n[ CÓDIGO: {ID} ] {Nome}");
            Console.WriteLine($"Telefone: {Telefone}");
            Console.WriteLine($"Email: {Email}\n");
        }

    }
}
