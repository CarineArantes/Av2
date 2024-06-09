using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Av2
{
    internal class Menu
    {
        private Dictionary<int, (Action acao, string descricao)> Opcoes;
        private Action ?Acao;
        private string ?Titulo;

        public Menu(string titulo, Action acao)
        {
            Titulo = titulo;
            Opcoes = new Dictionary<int, (Action, string)>();
            Acao = acao;
        }
        public Menu(string titulo)
        {
            Titulo = titulo;
            Opcoes = new Dictionary<int, (Action, string)>();
        }
        public Menu(Action acao)
        {
            Acao = acao;
            Opcoes = new Dictionary<int, (Action, string)>();
        }
        public Menu()
        {
            Opcoes = new Dictionary<int, (Action, string)>();
        }

        public void AddOpcao(int chave, Action acao, string descricao)
        {
            if (!Opcoes.ContainsKey(chave))
            {
                Opcoes.Add(chave, (acao, descricao));
            }
            else
            {
                throw new ArgumentException("A chave já existe no menu.");
            }
        }

        public void GerarMenu()
        {
            bool opcaoInvalida = false;
            do
            {
                Console.Clear();
                if (Titulo != null)
                    Console.WriteLine($"=== {Titulo} === \n");
                if (Acao != null) {
                    Acao();
                }
                foreach (var opcao in Opcoes)
                {
                    Console.WriteLine($"{opcao.Key}. {opcao.Value.descricao}");
                }

                Console.Write($"\nSelecione uma opção{(opcaoInvalida ? " válida" : "")}: ");

                // Verificando a escolha   
                if (int.TryParse(Console.ReadLine(), out int escolha))
                {
                    if (Opcoes.ContainsKey(escolha))
                    {
                        // Executa a função correspondente à escolha
                        Opcoes[escolha].acao.Invoke();
                        opcaoInvalida = false;
                    }
                    else
                    {
                        opcaoInvalida = true;
                    }
                }
                else
                {
                    opcaoInvalida = true;
                }
            } while (opcaoInvalida);
        }

    }
}
