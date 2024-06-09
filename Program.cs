using System.Diagnostics;
using System.Runtime.Intrinsics.X86;

namespace Av2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            Menu menu = new Menu("Seu Contato Fácil");
            menu.AddOpcao(1, Contatos, " Ver Contatos");
            menu.AddOpcao(2, Grupos, " Ver Grupos");
            menu.AddOpcao(3, Sair, " Sair");
            menu.GerarMenu();
        }


        static void Contatos()
        {
            Menu menuContatos = new Menu(ExibirContatos);
            menuContatos.AddOpcao(1, AddContato, " Adicionar Novo Contato");
            menuContatos.AddOpcao(2, EditContato, " Editar Contato");
            menuContatos.AddOpcao(3, DelContato, " Excluir Contato");
            menuContatos.AddOpcao(4, Voltar, " Voltar");
            menuContatos.GerarMenu();
        }
        static void ExibirContatos()
        {
            try
            {
                Gerenciador gerenciador = new Gerenciador();
                var contatosJSON = gerenciador.BuscaContatos();
                if (contatosJSON.Dados.Count > 0)
                {
                    foreach (var item in contatosJSON.Dados)
                    {
                        item.Exibir();
                    }
                }
                else
                {
                    Console.WriteLine("\n [ Não há contatos Cadastrados ]");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Write("\n");
        }
        static void AddContato()
        {
            Console.Clear();

            bool erro = false;
            string Nome = "", Telefone = "", Email = "";
            Console.Write("Informe os dados do novo contato: \n");

            do
            {
                try
                {
                    if (string.IsNullOrEmpty(Nome))
                    {
                        Console.Write("\n=> Nome: ");
                        string _Nome = Console.ReadLine();
                        if (_Nome?.Length >= 3 && _Nome.Length <= 200)
                        {
                            Nome = _Nome;
                        }
                        else {
                            throw new Exception("* O Nome precisa ter entre 3 e 200 caracteres");
                        }
                    }

                    if (string.IsNullOrEmpty(Telefone))
                    {

                        Console.Write("\n=> Telefone: ");
                        string _Telefone = Console.ReadLine();
                        if (_Telefone?.Length >= 8 && _Telefone.Length <= 13)
                        {
                            Telefone = _Telefone;
                        }
                        else
                        {
                            throw new Exception("* O Telefone precisa ter entre 8 e 13 caracteres");
                        }
                    }
                    if (string.IsNullOrEmpty(Email))
                    {
                        Console.Write("\n=> E-mail: ");
                        string _Email = Console.ReadLine();
                        if (_Email?.Length >= 13 && _Email.Length <= 50)
                        {
                            Email = _Email;
                        }
                        else
                        {
                            throw new Exception("* O E-mail precisa ter entre 10 e 50 caracteres");
                        }
                    }

                    Contato contato = new Contato(Nome, Telefone, Email);
                    Gerenciador gerenciador = new Gerenciador();
                    gerenciador.Salvar(contato);
                    Console.WriteLine("\n=== [ Contato adicionado com sucesso ! ] ===");
                    Console.WriteLine("\nPrecione [ ENTER ] para voltar ao menu de contatos");
                    Console.ReadLine();
                    Contatos();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    erro = true;
                }
            } while (erro);
            Contatos();

        }
        static void EditContato()
        {
            Console.Clear();
            bool erro = false;
            Gerenciador gerenciador = new Gerenciador();
            Console.Write("Informe o código do Contato: ");
            int contatoID = 0;
            bool houveAlteracao = false;
            bool etapa1 = false, etapa2 = false, etapa3 = false;
            do
            {
                try {
                    if (contatoID == 0)
                    {
                        if (int.TryParse(Console.ReadLine(), out int idSelecionado))
                        {
                            contatoID = idSelecionado;
                        }
                        else
                        {
                            Console.Clear();
                            throw new ArgumentException("Informe um código valido: ");
                        }
                    }
                    if(contatoID > 0) {
                        Contato contato = gerenciador.BuscaContatos(contatoID);
                        if (contato == null)
                        {
                            Console.WriteLine($"\n=== [ O código {contatoID} não foi encontrado ] ===\n");
                            Console.WriteLine("\nPrecione [ ENTER ] para voltar ao menu de contatos !");
                            Console.ReadLine();
                            Contatos();
                        }    
                        if (!erro) {
                            Console.Clear();
                            Console.WriteLine($"[ Contato {contatoID} Selecionado ]");
                        }

                        if (etapa1 == false) {
                            Console.WriteLine($"\n\nNome atual: {contato?.Nome}");
                            Console.Write("Deseja atualizar o Nome? \nPressione [S] para SIM ou qualquer outra para NÃO: ");

                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S')
                            {
                                Console.Write("\n=> Novo Nome: ");
                                contato.Nome = Console.ReadLine(); // Atualiza o nome do contato
                                houveAlteracao = true;
                                etapa1 = true;
                            }
                            else
                            {
                                etapa1 = true;
                            }
                        }
                        if (etapa2 == false) {
                            Console.WriteLine($"\n\nTelefone atual: {contato?.Telefone}");
                            Console.Write("Deseja atualizar o Telefone? \nPressione [S] para SIM qualquer outra para NÃO: ");
                            ConsoleKeyInfo keyInfo1 = Console.ReadKey();
                            if (keyInfo1.KeyChar == 's' || keyInfo1.KeyChar == 'S')
                            {
                                Console.Write("\n=> Novo Telefone: ");
                                contato.Telefone = Console.ReadLine(); // Atualiza o telefone do contato
                                houveAlteracao = true;
                                etapa2 = true;
                            }
                            else
                            {
                                etapa2 = true;
                            }
                        }
                        if (etapa3 == false)
                        {
                            Console.WriteLine($"\n\nE-mail atual: {contato?.Email}");
                            Console.Write("Deseja atualizar o E-mail? \nPressione [S] para SIM qualquer outra para NÃO: ");
                            ConsoleKeyInfo keyInfo2 = Console.ReadKey();
                            if (keyInfo2.KeyChar == 's' || keyInfo2.KeyChar == 'S')
                            {
                                Console.Write("\n=> Novo E-mail: ");
                                contato.Email = Console.ReadLine(); // Atualiza o e-mail do contato 
                                houveAlteracao = true;
                                etapa3 = true;
                            }
                            else
                            {
                                Console.Write("\n");
                                etapa3 = true;
                            }
                        }
                        if (houveAlteracao)
                        {
                            gerenciador.Salvar(contato);
                            Console.WriteLine("\n=== [ Contato atualizado com sucesso ! ] ===");
                        }

                        Console.WriteLine("\nPrecione [ ENTER ] para voltar ao menu de contatos !");
                        Console.ReadLine();
                        Contatos();
                    }
                    
                } catch (Exception ex) { 
                    Console.Write($"* {ex.Message}");
                    erro = true;
                };               
            }
            while (erro);
        }
        static void DelContato()
        {
            Console.Clear();
            bool erro = false;
            Gerenciador gerenciador = new Gerenciador();
            Console.Write("Informe o código do Contato a ser Excluido: ");
            int contatoID = 0;
            do
            {
                try
                {
                    if (contatoID == 0)
                    {
                        if (int.TryParse(Console.ReadLine(), out int idSelecionado))
                        {
                            contatoID = idSelecionado;
                        }
                        else
                        {
                            throw new ArgumentException("Informe um código valido: ");
                        }   
                    }
                    if (contatoID > 0)
                    {
                        Contato contato = gerenciador.BuscaContatos(contatoID);
                        if (contato == null)
                        {
                            Console.WriteLine($"\n=== [ O código {contatoID} não foi encontrado ] ===\n");
                            Console.WriteLine("\nPrecione [ ENTER ] para voltar ao menu de contatos !");
                            Console.ReadLine();
                            Contatos();
                        }
                        Console.WriteLine($"\n[ Contato {contatoID} Selecionado ]");
                        Console.WriteLine($"\n** Deseja realmente excluir o Contato {contatoID} ? **");
                        Console.Write("Pressione [S] para SIM ou qualquer outra para NÃO: ");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S') {
                            contato.Ativo = false;
                            gerenciador.Salvar(contato);
                            Console.WriteLine("\n=== [ Contato excluido com sucesso ! ] ===");
                        }
                        Console.WriteLine("\n\nPrecione [ ENTER ] para voltar ao menu de contatos !");
                        Console.ReadLine();
                        Contatos();
                    }
                }
                catch (Exception ex) {
                    Console.Write($"* {ex.Message}");
                    erro = true;
                };
                

            } while (erro);
        }





        static void Grupos()
        {

        }

        static void Sair()
        {
            Console.WriteLine($"\nDeseja realmente sair ?");
            Console.Write("Pressione [S] para SIM ou qualquer outra para NÃO: ");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S')
            {
                Environment.Exit(0);
            }
            MenuPrincipal();
            
        }
        static void Voltar()
        {
            MenuPrincipal();
        }

    }
}