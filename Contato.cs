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
        private string _Nome;
        private string _Telefone;
        private string _Email;
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
        public string Nome
        {
            get
            { return _Nome.ToUpper(); }
            set
            {
                _Nome = value;
            }
        }
        public string Telefone
        {
            get
            { return _Telefone; }
            set
            {
                _Telefone = value;
            }
        }
        public string Email
        {
            get
            { return _Email; }
            set
            {
                _Email = value;
            }

        }

        public bool Ativo
        {
            get
            { return _Ativo; }
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
            Console.WriteLine($"\n|CÓDIGO: {ID}| {Nome}");
            Console.WriteLine($"Telefone: {Telefone}");
            Console.WriteLine($"Email: {Email}\n");
        }

    }
}
