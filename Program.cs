using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace av2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            var contato = new Contato
            {
                idContato = 1,
                nome = "Carine",
                telefone = 17992148282,
                email = "",
                fkidgrupo = null,
            };


            List<Contato> list = new List<Contato>();
            list.Add(contato);
            */
            //cria a pasta no AppData
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SeuContatoFacil");
            //verifica se a pasta existe
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path); //cria a pasta caso não exista
            /*
            string fileName = Path.Combine(path, "Contato.json"); //nome do arquivo
            string jsonString = JsonSerializer.Serialize(list); //adiciona no json
            File.WriteAllText(fileName, jsonString);

            Console.WriteLine(File.ReadAllText(fileName));
            */

            string fileName = Path.Combine(path, "Contato.json");
            List<Contato> list;

            if (File.Exists(fileName))
            {
                string existingJson = File.ReadAllText(fileName);
                list = JsonSerializer.Deserialize<List<Contato>>(existingJson);
            }
            else
            {
                list = new List<Contato>();
            }
            //---------------------------------------------
            var contato3 = new Contato
            {
                idContato = 3,
                nome = "PC",
                telefone = 17999999999,
                email = "",
                fkidgrupo = [1],
            };

            var contatoToEdit = list.FirstOrDefault(c => c.idContato == 2);

            if (contatoToEdit != null)
            {
                contatoToEdit.idContato = 2;
                contatoToEdit.nome = "Pedro Bobao Lindao";
                string jsonString = JsonSerializer.Serialize(list);
                File.WriteAllText(fileName, jsonString);

                Console.WriteLine("Deu certo");
            }

            /*
            list.Add(contato3);
            string jsonString = JsonSerializer.Serialize(list); //adiciona no json
            File.WriteAllText(fileName, jsonString);
            */
            /*
            var contatoToRemove = list.FirstOrDefault(c => c.idContato == 3);

            if (contatoToRemove != null)
            {
                list.Remove(contatoToRemove);

                string jsonString = JsonSerializer.Serialize(list);
                File.WriteAllText(fileName, jsonString);

                Console.WriteLine("Deu certo");
            }
            else
            {
                Console.WriteLine("Contato com o ID especificado não encontrado.");
            }
            */
        }
    }
}