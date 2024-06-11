using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;


namespace Av2
{
    internal class Gerenciador
    {

        private readonly string CaminhoPasta;
        private readonly string CaminhoContato;
        private readonly string CaminhoGrupo;

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
        private static EntidadeJSON<T> BuscaDados<T>(string caminho) {
            try
            {
                string stringJSONS = File.ReadAllText(caminho);
                var dadosJSON = JsonSerializer.Deserialize<EntidadeJSON<T>>(stringJSONS);
                return dadosJSON ?? new EntidadeJSON<T>();
            }
            catch{
                throw new Exception("Erro ao buscar dados !");
            }  
        }
        private static void SalvaDados<T>(string caminho, EntidadeJSON<T> dadosJSON)
        {
            try
            {
                string stringJSONS = File.ReadAllText(caminho);
                stringJSONS = JsonSerializer.Serialize(dadosJSON);
                File.WriteAllText(caminho, stringJSONS);
            }
            catch
            {
                throw new Exception("Erro ao salvar dados !");
            }
        }



        public EntidadeJSON<Contato> BuscaContatos()
        {
            try
            {
                string stringJSONS = File.ReadAllText(CaminhoContato);
                var contatosJSON = JsonSerializer.Deserialize<EntidadeJSON<Contato>>(stringJSONS);
                // Verificar se contatosJSON é nulo ou não possui dados
                if (contatosJSON == null || contatosJSON.Dados == null)
                {
                    return new EntidadeJSON<Contato>();
                }
                // Filtrar todos os contatos ativos
                var contatosAtivos = contatosJSON.Dados.Where(p => p.Ativo).ToList();
                // Criar um novo objeto EntidadeJSON com os contatos ativos encontrados
                var resultado = new EntidadeJSON<Contato>
                {
                    Dados = contatosAtivos
                };
                return resultado;
            }
            catch
            {
                throw new Exception("Erro ao Buscar Contatos!");
            }
        }
        public Contato BuscaContatos(int id)
        {
            try
            {
                var contatosJSON = BuscaContatos();
                var contato = contatosJSON.Dados.FirstOrDefault(p => p.ID == id);
                return contato;
            }
            catch{
                throw new Exception("Erro ao Buscar Contatos !");
            }   
        }

        public EntidadeJSON<Grupo> BuscaGrupos()
        {
            try
            {
                string stringJSONS = File.ReadAllText(CaminhoGrupo);
                var gruposJSON = JsonSerializer.Deserialize<EntidadeJSON<Grupo>>(stringJSONS);
                // Verificar se gruposJSON é nulo ou não possui dados
                if (gruposJSON == null || gruposJSON.Dados == null)
                {
                    return new EntidadeJSON<Grupo>();
                }
                // Filtrar todos os contatos ativos
                var gruposAtivos = gruposJSON.Dados.Where(p => p.Ativo).ToList();
                // Criar um novo objeto EntidadeJSON com os grupos ativos encontrados
                var resultado = new EntidadeJSON<Grupo>
                {
                    Dados = gruposAtivos
                };
                return resultado;
            }
            catch
            {
                throw new Exception("Erro ao Buscar Grupos!");
            }
        }
        public Grupo BuscaGrupos(int id)
        {
            try
            {
                var grupoJSON = BuscaGrupos();
                var grupo = grupoJSON.Dados.FirstOrDefault(p => p.ID == id);

                return grupo;
            }
            catch
            {
                throw new Exception("Erro ao Buscar Grupo !");
            }
        }


        public static bool  IDeNulo<T>(T item)
        {
            PropertyInfo propriedadeId = typeof(T).GetProperty("ID");
            if (propriedadeId != null)
            {
                object id = propriedadeId.GetValue(item);
                return id == null || (id is int && (int)id == 0);
            }
            return true;
        }
        public void Salvar(Contato contato)
        {
            Salvar<Contato>(contato, CaminhoContato);
        }
        public void Salvar(Grupo grupo)
        {
            Salvar<Grupo>(grupo, CaminhoGrupo);
        }
        private void Salvar<T>(T item, string caminho)
        {
            try
            {
                if (item != null) {
                    if (IDeNulo<T>(item)) {
                        AdicionarItem(item, caminho);
                    }
                    else {
                        AtualizarItem(item, caminho);
                    }
                }
                else{
                    throw new Exception("Erro ao salvar !");
                }            
            }
            catch(Exception ex) {
                throw new Exception(ex.Message ?? "Error ao salvar");
            }   
        }
        private static void AdicionarItem<T>(T item, string caminho) {
            try
            {
                var dadosJSON = BuscaDados<T>(caminho);
                int novoID = dadosJSON.UltimoAdicionado + 1;
                item?.GetType()?.GetProperty("ID")?.SetValue(item, novoID);
                dadosJSON.UltimoAdicionado = novoID;
                dadosJSON.Dados.Add(item);
                SalvaDados<T>(caminho, dadosJSON);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message ?? "Error ao salvar");
            }        
        }
        private static void AtualizarItem<T>(T item, string caminho)
        {
            try
            {
                var dadosJSON = BuscaDados<T>(caminho);
                PropertyInfo propriedadeId = typeof(T).GetProperty("ID");
                if (propriedadeId != null)
                {
                    object itemID = propriedadeId.GetValue(item);
                    var itemExistente = dadosJSON.Dados.FirstOrDefault(x => propriedadeId.GetValue(x).Equals(itemID));
                    if (itemExistente != null)
                    {
                        int indice = dadosJSON.Dados.IndexOf(itemExistente);
                        dadosJSON.Dados[indice] = item;
                    }
                    else
                    {
                        dadosJSON.Dados.Add(item);
                    }
                    SalvaDados<T>(caminho, dadosJSON);
                }
            }
            catch (Exception ex)
            {
                do { } while (true);
                throw new Exception(ex.Message ?? "Error ao Atualizar");
            }
        }

    }
}
