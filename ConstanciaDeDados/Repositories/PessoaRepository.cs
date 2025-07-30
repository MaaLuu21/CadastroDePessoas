using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConstanciaDeDados.Entities;
using ConstanciaDeDados.Exceptions;

namespace ConstanciaDeDados.Repositories
{
    public class PessoaRepository
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
                throw new DomainException("Erro ao salvar o arquivo " + e.Message);
            }
        }
    }
}
