using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Av2
{
    internal class Grupo
    {
        private int _ID;
        private string _Nome;
        private string _Descricao;
        private bool _Ativo;
        private List<int> _IDContatos;


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
            get
            { return _Nome.ToUpper(); }
            set
            {

                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O Nome não pode ser vazio.");
                }
                if (value.Length < 5 && value.Length > 50)
                {
                    throw new ArgumentException("O Nome precisa ter entre 5 e 50 caracteres .");
                }
                _Nome = value;
            }
        }
        public string Descricao
        {
            get
            { return _Descricao.ToUpper(); }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A Descrição não pode ser vazio.");
                }
                if (value.Length < 5 && value.Length > 70)
                {
                    throw new ArgumentException("A Descrição precisa ter entre 5 e 50 caracteres .");
                }
                _Descricao = value;
            }
        }
        public List<int> IDContato
        {
            get { return _IDContatos; }

            set { _IDContatos = value; }
        }


        public Grupo(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
            _IDContatos = new List<int>();
            Ativo = true;
        }

        // Método para adicionar um ID de Contato
        public void AdicionarContato(int idContato)
        {
            if (_IDContatos.Contains(idContato))
            {
                _IDContatos.Remove(idContato);
            }
            _IDContatos.Add(idContato);
        }

        // Método para remover um ID de Contato
        public void RemoverContato(int idContato)
        {
            _IDContatos.Remove(idContato);
        }

        public void Exibir()
        {
            Console.WriteLine($"\n[CÓDIGO: {ID}] {Nome}");
            Console.WriteLine($"- {Descricao}");
        }

    }
}
