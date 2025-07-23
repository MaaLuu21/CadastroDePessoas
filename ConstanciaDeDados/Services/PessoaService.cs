using System;
using System.IO;
using System.Text.Json;
using ConstanciaDeDados.Entities;

namespace ConstanciaDeDados.Services
{
    public static class PessoaService{
        private static string caminhoArquivo = "pessoas.json";
        public static List<Pessoa> Carregar()
        {
            if (File.Exists(caminhoArquivo))
            {
                string json = File.ReadAllText(caminhoArquivo);
                return JsonSerializer.Deserialize<List<Pessoa>>(json) ?? new List<Pessoa>();
            }
            return new List<Pessoa>();
        }
        public static void Salvar(List<Pessoa> pessoas)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(pessoas, options);
            File.WriteAllText(caminhoArquivo, json);
        }
        public static void Remover(string nome)
        {
            var pessoas = Carregar();

            var pessoa = pessoas.Find(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        
            if (pessoa != null)
            {
                pessoas.Remove(pessoa);
                Salvar(pessoas);
                Console.WriteLine($"Pessoa '{nome}' removidacom sucesso");
            }
            else
            {
                Console.WriteLine($"Pessoa '{nome}' não encontrada");
            }
        }
    }
}
