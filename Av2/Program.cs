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
            menuContatos.AddOpcao(1, NovoContato, " Novo Contato");
            menuContatos.AddOpcao(2, EditarContato, " Editar Contato");
            menuContatos.AddOpcao(3, ExcluirContato, " Excluir Contato");
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
                    Console.WriteLine("\n [ -- Não há Contatos Cadastrados -- ]");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Write("\n");
        }
        static void NovoContato()
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
                        if (string.IsNullOrWhiteSpace(_Nome))
                        {
                            throw new ArgumentException("* O Nome não pode ser vazio.");
                        }
                        if (_Nome.Length < 2 || _Nome.Length > 100)
                        {
                            throw new ArgumentException("* O Nome precisa ter entre 2 e 100 caracteres");

                        }
                        Nome = _Nome;
                    }


                    if (string.IsNullOrEmpty(Telefone))
                    {
                        Console.Write("\n=> Telefone: ");
                        string _Telefone = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(_Telefone))
                        {
                            throw new ArgumentException("* O Telefone não pode ser vazio.");
                        }
                        if (_Telefone.Length < 8 || _Telefone.Length > 13 || !_Telefone.All(char.IsDigit))
                        {
                            throw new ArgumentException("* O Telefone precisa ter entre 8 e 13 caracteres e conter apenas números.");
                        }
                        Telefone = _Telefone;
                    }
                    if (string.IsNullOrEmpty(Email))
                    {
                        Console.Write("\n=> E-mail: ");
                        string _Email = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(_Email))
                        {
                            throw new ArgumentException("* O  E-mail não pode ser vazio.");
                        }
                        if (_Email.Length < 10 || _Email.Length > 100)
                        {
                            throw new ArgumentException("* O E-mail precisa ter entre 10 e 100 caracteres.");
                        }
                        Email = _Email;
                    }

                    Contato contato = new Contato(Nome, Telefone, Email);
                    Gerenciador gerenciador = new Gerenciador();
                    gerenciador.Salvar(contato);
                    Console.WriteLine("\n=== [ Contato criado com sucesso ! ] ===");
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
        static void EditarContato()
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

                        Console.WriteLine("\n\nPrecione [ ENTER ] para voltar ao menu de contatos !");
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
        static void ExcluirContato()
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
                        Console.WriteLine($"\n[ Contato {contato?.Nome} Selecionado ]");
                        Console.WriteLine($"\n** Deseja realmente excluir o Contato {contatoID} ? **");
                        Console.WriteLine($"** ele será removido de todos os grupos **\n");
                        Console.Write("Pressione [S] para SIM ou qualquer outra para NÃO: ");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S') {
                            contato.Ativo = false;
                            gerenciador.Salvar(contato);
                            Console.WriteLine("\n\n=== [ Contato foi excluido com sucesso ! ] ===");
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
        static void ExibirGrupos()
        {
            try
            {
                Gerenciador gerenciador = new Gerenciador();
                var gruposJSON = gerenciador.BuscaGrupos();
                if (gruposJSON.Dados.Count > 0)
                {
                    foreach (var item in gruposJSON.Dados)
                    {
                        item.Exibir();
                    }
                }
                else
                {
                    Console.WriteLine("\n [ -- Não há Grupos Cadastrados -- ]");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Write("\n");
        }
        static void Grupos()
        {
            Menu menuGrupos = new Menu(ExibirGrupos);
            menuGrupos.AddOpcao(1, DetalhesGrupo, " Detalhes do Grupo");
            menuGrupos.AddOpcao(2, NovoGrupo, " Novo Grupo");
            menuGrupos.AddOpcao(3, EditarGrupo, " Editar Grupo");
            menuGrupos.AddOpcao(4, ExcluirGrupo, " Excluir Grupo");
            menuGrupos.AddOpcao(5, Voltar, " Voltar");
            menuGrupos.GerarMenu();
        }
        static void NovoGrupo()
        {
            Console.Clear();

            bool erro = false;
            string Nome = "", Descricao = "";
            List<int> contatosIDs = new List<int>();
            Console.Write("Informe os dados do novo grupo: \n");

            do
            {
                try
                {
                    if (string.IsNullOrEmpty(Nome))
                    {
                        Console.Write("\n=> Nome: ");
                        string _Nome = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(_Nome))
                        {
                            throw new ArgumentException("* O Nome não pode ser vazio.");
                        }
                        if (_Nome.Length < 5 || _Nome.Length > 50)
                        {
                            throw new ArgumentException("* O Nome precisa ter entre 5 e 50 caracteres");

                        }
                        Nome = _Nome;
                    }

                    if (string.IsNullOrEmpty(Descricao))
                    {
                        Console.Write("\n=> Descrição: ");
                        string _Descricao = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(_Descricao))
                        {
                            throw new ArgumentException("* A Descrição não pode ser vazio.");
                        }
                        if (_Descricao.Length < 5 || _Descricao.Length > 70 )
                        {
                            throw new ArgumentException("* A Descrição precisa ter entre 5 e 13 caracteres e conter apenas números.");
                        }
                        Descricao = _Descricao;
                    }

                    Grupo grupo = new Grupo(Nome, Descricao);
                    Gerenciador gerenciador = new Gerenciador();
                    gerenciador.Salvar(grupo);
                    Console.WriteLine("\n=== [ Grupo criado com sucesso ! ] ===");
                    Console.WriteLine("\nPrecione [ ENTER ] para voltar ao menu de grupos");
                    Console.ReadLine();
                    Grupos();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    erro = true;
                }
            } while (erro);
            Contatos();

        }
        static void EditarGrupo()
        {
            Console.Clear();
            bool erro = false;
            Gerenciador gerenciador = new Gerenciador();
            Console.Write("Informe o código do Grupo: ");
            int grupoID = 0;
            bool houveAlteracao = false;
            bool etapa1 = false, etapa2 = false;
            do
            {
                try
                {
                    if (grupoID == 0)
                    {
                        if (int.TryParse(Console.ReadLine(), out int idSelecionado))
                        {
                            grupoID = idSelecionado;
                        }
                        else
                        {
                            Console.Clear();
                            throw new ArgumentException("Informe um código valido: ");
                        }
                    }
                    if (grupoID > 0)
                    {
                        Grupo grupo = gerenciador.BuscaGrupos(grupoID);
                        if (grupo == null)
                        {
                            Console.WriteLine($"\n=== [ O código {grupoID} não foi encontrado ] ===\n");
                            Console.WriteLine("\nPrecione [ ENTER ] para voltar ao menu de grupos !");
                            Console.ReadLine();
                            Grupos();
                        }
                        if (!erro)
                        {
                            Console.Clear();
                            Console.WriteLine($"[ Grupo {grupoID} Selecionado ]");
                        }

                        if (etapa1 == false)
                        {
                            Console.WriteLine($"\n\nNome atual: {grupo?.Nome}");
                            Console.Write("Deseja atualizar o Nome? \nPressione [S] para SIM ou qualquer outra para NÃO: ");

                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S')
                            {
                                Console.Write("\n=> Novo Nome: ");
                                grupo.Nome = Console.ReadLine(); // Atualiza o nome do grupo
                                houveAlteracao = true;
                                etapa1 = true;
                            }
                            else
                            {
                                etapa1 = true;
                            }
                        }
                        if (etapa2 == false)
                        {
                            Console.WriteLine($"\n\nDescrição atual: {grupo?.Descricao}");
                            Console.Write("Deseja atualizar a Descrição? \nPressione [S] para SIM qualquer outra para NÃO: ");
                            ConsoleKeyInfo keyInfo1 = Console.ReadKey();
                            if (keyInfo1.KeyChar == 's' || keyInfo1.KeyChar == 'S')
                            {
                                Console.Write("\n=> Nova Descrição: ");
                                grupo.Descricao = Console.ReadLine(); // Atualiza a Descrição do grupo
                                houveAlteracao = true;
                                etapa2 = true;
                            }
                            else
                            {
                                etapa2 = true;
                            }
                        }             
                        if (houveAlteracao)
                        {
                            gerenciador.Salvar(grupo);
                            Console.WriteLine("\n=== [ Grupo atualizado com sucesso ! ] ===");
                        }

                        Console.WriteLine("\n\nPrecione [ ENTER ] para voltar ao menu de grupos !");
                        Console.ReadLine();
                        Grupos();
                    }

                }
                catch (Exception ex)
                {
                    Console.Write($"* {ex.Message}");
                    erro = true;
                };
            }
            while (erro);
        }
        static void ExcluirGrupo()
        {
            Console.Clear();
            bool erro = false;
            Gerenciador gerenciador = new Gerenciador();
            Console.Write("Informe o código do Grupo a ser Excluido: ");
            int grupoID = 0;
            do
            {
                try
                {
                    if (grupoID == 0)
                    {
                        if (int.TryParse(Console.ReadLine(), out int idSelecionado))
                        {
                            grupoID = idSelecionado;
                        }
                        else
                        {
                            throw new ArgumentException("Informe um código valido: ");
                        }
                    }
                    if (grupoID > 0)
                    {
                        Grupo grupo = gerenciador.BuscaGrupos(grupoID);

                        if (grupo == null)
                        {
                            Console.WriteLine($"\n=== [ O código {grupoID} não foi encontrado ] ===\n");
                            Console.WriteLine("\nPrecione [ ENTER ] para voltar ao menu de grupos");
                            Console.ReadLine();
                            Grupos();
                        }
                        Console.WriteLine($"\n[ Grupo {grupo?.Nome} Selecionado ]");
                        Console.WriteLine($"\n** Deseja realmente excluir o Grupo {grupoID} ? **");
                        Console.Write("Pressione [S] para SIM ou qualquer outra para NÃO: ");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S')
                        {
                            grupo.Ativo = false;
                            gerenciador.Salvar(grupo);
                            Console.WriteLine("\n\n=== [ Grupo foi excluido com sucesso ! ] ===");
                        }
                        Console.WriteLine("\n\nPrecione [ ENTER ] para voltar ao menu de grupos !");
                        Console.ReadLine();
                        Grupos();
                    }
                }
                catch (Exception ex)
                {
                    Console.Write($"* {ex.Message}");
                    erro = true;
                };


            } while (erro);
        }

        static void VerContatosGrupo(Grupo grupo) {
            Console.WriteLine($"\n-- {grupo.Nome} --");
            Console.WriteLine($"{grupo.Descricao} \n");
            Console.WriteLine($"* Contatos no Grupo *");
            if (grupo.IDContato.Count > 0)
            {
                Gerenciador gerenciador = new Gerenciador();
                Contato contato;
                foreach (int id in grupo.IDContato)
                {
                    contato = gerenciador.BuscaContatos(id);
                    if (contato != null)
                        contato.Exibir();
                }
            }
            else {
                Console.WriteLine("[ -- Não há Contatos no Grupo -- ]");
            }
            Console.WriteLine("\n");
        }
        static void VerGrupo(Grupo grupo){
            Menu menuGrupos = new Menu(() => VerContatosGrupo(grupo));
            menuGrupos.AddOpcao(1,() => AdicionarNoGrupo(grupo), " Adicionar Contato ao grupo");
            menuGrupos.AddOpcao(2, () => RemoverDoGrupo(grupo), " Remover Contato do grupo");
            menuGrupos.AddOpcao(3, Grupos, " Voltar");
            menuGrupos.GerarMenu();
        }
        static void DetalhesGrupo() {
            Console.Clear();
            bool erro = false;
            Gerenciador gerenciador = new Gerenciador();
            Console.Write("Informe o código do Grupo : ");
            int grupoID = 0;
            Grupo grupo;
            do
            {
                try
                {
                    if (grupoID == 0)
                    {
                        if (int.TryParse(Console.ReadLine(), out int idSelecionado))
                        {
                            grupoID = idSelecionado;
                        }
                        else
                        {
                            throw new ArgumentException("Informe um código valido: ");
                        }
                    }
                    if (grupoID > 0)
                    {
                        grupo = gerenciador.BuscaGrupos(grupoID);
                        if (grupo == null)
                        {
                            Console.WriteLine($"\n=== [ O código {grupoID} não foi encontrado ] ===\n");
                            Console.WriteLine("\nPrecione [ ENTER ] para voltar ao menu de grupos");
                            Console.ReadLine();
                            Grupos();
                        }
                        else
                        {
                            VerGrupo(grupo);
                            erro = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write($"* {ex.Message}");
                    erro = true;
                }
            }while (erro);
        }
        static void AdicionarNoGrupo(Grupo grupo) {
            Console.Clear();
            bool adicionarNovamete = false, limparConsole = false;
            Gerenciador gerenciador = new Gerenciador();
            Console.WriteLine($"[ Grupo {grupo.Nome} selecionado: ]\n");
            Console.Write("Informe o código do Contato a ser Adicionado: ");
            int contatoID = 0;
            do
            {
                try
                {
                    if (limparConsole) {
                        Console.Clear();
                        Console.WriteLine($"[ Grupo {grupo.Nome} selecionado: ]\n");
                        Console.Write("Informe o código do Contato a ser Adicionado: ");
                    }
                    if (contatoID == 0)
                    {
                        if (int.TryParse(Console.ReadLine(), out int idSelecionado))
                            contatoID = idSelecionado;
                        else
                            throw new ArgumentException("Informe um código valido: ");
                    }
                    Contato contato = gerenciador.BuscaContatos(contatoID);
                    if (contato == null)
                    {
                        Console.WriteLine($"\n=== [ O código {contatoID} não foi encontrado ] ===\n");
                        Console.Write("\nPressione [S] para VOLTAR ou qualquer outra para TENTAR NOVAMENTE: ");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        contatoID = 0;
                        if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S')
                        {
                            adicionarNovamete = false;
                            VerGrupo(grupo);
                        }
                        else
                        {
                            adicionarNovamete = true;
                            limparConsole = true;
                        }
                    }
                    else {
                        Console.WriteLine($"\nContato [ {contato.Nome} ] selecionado\n");
                        Console.WriteLine("=> Deseja adicionar ao Grupo ?");
                        Console.Write("Pressione [S] para SIM ou qualquer outra para NÃO : ");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S')
                        {
                            adicionarNovamete = false;
                            grupo.AdicionarContato(contato.ID);
                            gerenciador.Salvar(grupo);
                            Console.WriteLine("\n\n=== [ Contato adicionado ao grupo com sucesso ! ] ===");

                        }
                        Console.Write("\nPressione [S] para VOLTAR ou qualquer outra para ADICIONAR CONATO AO GRUPO: ");
                        keyInfo = Console.ReadKey();
                        contatoID = 0;
                        if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S')
                        {
                            adicionarNovamete = false;
                            VerGrupo(grupo);
                        }
                        else
                        {
                            adicionarNovamete = true;
                            limparConsole = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write($"* {ex.Message}");
                    adicionarNovamete = true;
                    limparConsole = false;
                    contatoID = 0;
                }
            } while (adicionarNovamete);
            
        }
        static void RemoverDoGrupo(Grupo grupo)
        {
            Console.Clear();
            bool adicionarNovamete = false, limparConsole = false;
            Gerenciador gerenciador = new Gerenciador();
            Console.WriteLine($"[ Grupo {grupo.Nome} selecionado: ]\n");
            Console.Write("Informe o código do Contato a ser Removido: ");
            int contatoID = 0;
            do
            {
                try
                {
                    if (limparConsole)
                    {
                        Console.Clear();
                        Console.WriteLine($"[ Grupo {grupo.Nome} selecionado: ]\n");
                        Console.Write("Informe o código do Contato a ser Removido: ");
                    }
                    if (contatoID == 0)
                    {
                        if (int.TryParse(Console.ReadLine(), out int idSelecionado))
                            contatoID = idSelecionado;
                        else
                            throw new ArgumentException("Informe um código valido: ");
                    }
                    Contato contato = gerenciador.BuscaContatos(contatoID);
                    if (contato == null)
                    {
                        Console.WriteLine($"\n=== [ O código {contatoID} não foi encontrado ] ===\n");
                        Console.Write("\nPressione [S] para VOLTAR ou qualquer outra para TENTAR NOVAMENTE: ");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        contatoID = 0;
                        if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S')
                        {
                            adicionarNovamete = false;
                            VerGrupo(grupo);
                        }
                        else
                        {
                            adicionarNovamete = true;
                            limparConsole = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nContato [ {contato.Nome} ] selecionado\n");
                        Console.WriteLine("=> Deseja remover do Grupo ?");
                        Console.Write("Pressione [S] para SIM ou qualquer outra para NÃO : ");
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S')
                        {
                            adicionarNovamete = false;
                            grupo.RemoverContato(contato.ID);
                            gerenciador.Salvar(grupo);
                            Console.WriteLine("\n\n=== [ Contato removido do grupo com sucesso ! ] ===");

                        }
                        Console.Write("\nPressione [S] para VOLTAR ou qualquer outra para REMOVER CONATO DO GRUPO: ");
                        keyInfo = Console.ReadKey();
                        contatoID = 0;
                        if (keyInfo.KeyChar == 's' || keyInfo.KeyChar == 'S')
                        {
                            adicionarNovamete = false;
                            VerGrupo(grupo);
                        }
                        else
                        {
                            adicionarNovamete = true;
                            limparConsole = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write($"* {ex.Message}");
                    adicionarNovamete = true;
                    limparConsole = false;
                    contatoID = 0;
                }
            } while (adicionarNovamete);

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