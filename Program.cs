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

            //MenuPrincipal();






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
            Gerenciador gerenciador = new Gerenciador();
            var contatosJSON = gerenciador.BuscaContatos();
        }

        static void Grupos()
        {

        }

        static void Sair()
        {

        }

    }
}