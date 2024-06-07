using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace Av2
{
    internal class Gerenciador
    {

        private string CaminhoPasta;
        private string CaminhoContato;
        private string CaminhoGrupo;

        public Gerenciador()
        {
            CaminhoPasta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SeuContatoFacil");
            CaminhoContato = Path.Combine(CaminhoPasta, "Contatos.json");
            CaminhoGrupo = Path.Combine(CaminhoPasta, "Grupos.json");
            VerificaPasta();
        }

        private void VerificaPasta()
        {
            if (!Directory.Exists(CaminhoPasta))
                Directory.CreateDirectory(CaminhoPasta); //cria a pasta caso não exista
            CriaArquivo(CaminhoContato);
            CriaArquivo(CaminhoGrupo);
        }

        static void CriaArquivo(string caminhoArquivo)
        {
            if (!File.Exists(caminhoArquivo))
            {
                using (File.Create(caminhoArquivo)) { } // Usando 'using' para garantir que o recurso seja liberado
                string conteudo = "{\n    \"Dados\": [],\n    \"UltimoAdicionado\": 0\n}";
                File.WriteAllText(caminhoArquivo, conteudo);
            }
        }

        public Contato BuscaContato(int id)
        {
            string stringJSONS = File.ReadAllText(CaminhoContato);
            var contatosJSON = JsonSerializer.Deserialize<EntidadeJSON<Contato>>(stringJSONS);
            var contato = contatosJSON.Dados.FirstOrDefault(p => p.ID == id && p.Ativo == true);
            Debug.WriteLine(contato);
            if (contato != null)
            {
                throw new Exception("Contato não encontrado");
            }
            return contato;

        }

        public EntidadeJSON<Contato> BuscaContatos()
        {
            string stringJSONS = File.ReadAllText(CaminhoContato);
            var contatosJSON = JsonSerializer.Deserialize<EntidadeJSON<Contato>>(stringJSONS);
            return contatosJSON ?? new EntidadeJSON<Contato>();

        }

        public void Salvar(Contato contato)
        {
            Salvar<Contato>(contato, CaminhoContato);
        }

        public void Salvar(Grupo grupo)
        {
            Salvar<Grupo>(grupo, CaminhoGrupo);
        }


        private static void Salvar<T>(T item, string caminho)
        {
            string stringJSONS = File.ReadAllText(caminho);
            var dadosJSON = JsonSerializer.Deserialize<EntidadeJSON<T>>(stringJSONS);
            int id = dadosJSON.UltimoAdicionado + 1;
            item.GetType().GetProperty("ID").SetValue(item, id);
            dadosJSON.UltimoAdicionado = id;
            dadosJSON.Dados.Add(item);
            stringJSONS = JsonSerializer.Serialize(dadosJSON);
            File.WriteAllText(caminho, stringJSONS);
        }
    }
}
