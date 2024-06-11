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

        public string Nome
        {
            get { return _Nome.ToUpper(); }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O Nome não pode ser vazio.");
                }
                if (value.Length < 2 && value.Length > 100)
                {
                    throw new ArgumentException("* O Nome precisa ter entre 2 e 100 caracteres");
                   
                }
                _Nome = value;
            }
        }

        public string Telefone
        {
            get { return _Telefone; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O Telefone não pode ser vazio.");
                }
                if (value.Length < 8 && value.Length > 13 && !value.All(char.IsDigit))
                {
                    throw new ArgumentException("O Telefone precisa ter entre 8 e 13 caracteres e conter apenas números.");
                }
                _Telefone = value;
            }
        }

        public string Email
        {
            get { return _Email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O  E-mail não pode ser vazio.");
                }
                if (value.Length < 10 && value.Length > 100)
                {
                    throw new ArgumentException("O E-mail precisa ter entre 10 e 100 caracteres.");                 
                }
                _Email = value;
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

        public void Exibir()
        {
            Console.WriteLine($"\n[ CÓDIGO: {ID} ] {Nome}");
            Console.WriteLine($"Telefone: {Telefone}");
            Console.WriteLine($"Email: {Email}\n");
        }

    }
}
