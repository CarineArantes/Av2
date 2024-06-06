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
        private List<int> _IDContatos;


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

        public List<int> IDContato
        {
            get
            { return _IDContatos; }
        }


        public Grupo(string nome)
        {
            Nome = nome;
            _IDContatos = new List<int>();
        }

        // Método para adicionar um ID de Contato
        public void AddGrupo(int idContato)
        {
            _IDContatos.Add(idContato);
        }

        // Método para remover um ID de Contato
        public void RemoverGrupo(int idContato)
        {
            _IDContatos.Remove(idContato);
        }

        public void Exibir()
        {
            Console.WriteLine($"\n|CÓDIGO: {ID}| {Nome}");
        }

    }
}
