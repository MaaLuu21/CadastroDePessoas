using System;
using System.IO;
using System.Text.Json;
using ConstanciaDeDados.Entities;
using ConstanciaDeDados.Exceptions;

namespace ConstanciaDeDados.Services
{
    public static class PessoaService
    {
        private static string caminhoArquivo = "pessoas.json";
        public static List<Pessoa> Carregar()
        {
            if (File.Exists(caminhoArquivo))
            {
                string json = File.ReadAllText(caminhoArquivo);
                var pessoas = JsonSerializer.Deserialize<List<Pessoa>>(json) ?? new List<Pessoa>();

                return pessoas.OrderBy(p => p.Nome, StringComparer.OrdinalIgnoreCase).ToList();
            }
            return new List<Pessoa>();
        }
        public static void Salvar(List<Pessoa> pessoas)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(pessoas, options);
                File.WriteAllText(caminhoArquivo, json);

            }
            catch (DomainException e)
            {
                throw new DomainException ("Erro ao salvar o arquivo " + e.Message);
            }
        }
        public static void Remover(int id)
        {
            var pessoas = Carregar();

            var pessoa = pessoas.Find(p => p.Id == id);

            if (pessoa != null)
            {
                pessoas.Remove(pessoa);
                Salvar(pessoas);
                Console.WriteLine($"'{pessoa.Nome}' removido(a) com sucesso");
            }
            else
            {
                throw new DomainException ($"Pessoa '{pessoa.Nome}' não econtrada");
            }
        }
    }
}
