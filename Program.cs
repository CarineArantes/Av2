namespace Av2
{
    internal class Program
    {


        static void Main(string[] args)
        {
            /*
            Gerenciador gerenciador = new Gerenciador();

            Contato contato1 = new Contato("Pedro 1", 551799999999, "Pedro@pedro");
            Contato contato2 = new Contato("Carine 1", 551799999999, "carine@carine");
            Contato contato3 = new Contato("Pedro 2", 551799999999, "Pedro@pedro");
            Contato contato4 = new Contato("Carine 2", 551799999999, "carine@carine");

            gerenciador.Salvar(contato1);
            gerenciador.Salvar(contato2);
            gerenciador.Salvar(contato3);
            gerenciador.Salvar(contato4);

            */

            MenuPrincipal();






        }

        static void MenuPrincipal()
        {
            Menu menu = new Menu("Seu Contato Fácil");
            menu.AddOpcao(1, ExibirContatos, " Ver Contatos");
            menu.AddOpcao(2, Grupos, " Ver Grupos");
            menu.AddOpcao(3, Sair, " Sair");
            menu.GerarMenu();

        }

        static void ExibirContatos()
        {
            Console.Clear();
            Gerenciador gerenciador = new Gerenciador();
            var contatosJSON = gerenciador.BuscaContatos();

            foreach ( var item in contatosJSON.Dados )
            {
                item.Exibir();
            }

            Menu menuContatos = new Menu();
            menuContatos.AddOpcao(1, AddContato, " Adicionar Novo Contato");
            menuContatos.AddOpcao(2, EditContato, " Editar Contato");
            menuContatos.AddOpcao(3, DelContato, " Excluir Contato");
            menuContatos.AddOpcao(4, Voltar, " Voltar");
            menuContatos.GerarMenu();

       
        }

        static void Grupos()
        {

        }

        static void Sair()
        {

        }

        static void AddContato()
        {
            Console.WriteLine("Dados do Contato");
            Console.Write("Nome:");
            string Nome = Console.ReadLine();
            Console.Write("Telefone:");
            string Telefone = Console.ReadLine();
            Console.Write("E-mail:");
            string Email = Console.ReadLine();
            Contato contato = new Contato(Nome, Telefone, Email);
            Gerenciador gerenciador = new Gerenciador();
            gerenciador.Salvar(contato);
            ExibirContatos();
        }

        static void EditContato()
        {
            Gerenciador gerenciador = new Gerenciador();
            Console.WriteLine("Informe o Código do Contato");
            int.TryParse(Console.ReadLine(), out int id);
            try
            {
                var contato = gerenciador.BuscaContato(id);
                contato.Exibir();
            
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            /*
            Console.WriteLine("Dados do Contato");
            Console.Write("Nome:");
            string Nome = Console.ReadLine();
            Console.Write("Telefone:");
            string Telefone = Console.ReadLine();
            Console.Write("E-mail:");
            string Email = Console.ReadLine();
            ExibirContatos();
            */
        }

        static void DelContato()
        {

        }

        static void Voltar()
        {

        }

    }
}