using System.Text.Json;
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
            catch (IOException e)
            {
                throw new DomainException("Erro ao salvar o arquivo: " + e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                throw new DomainException("Sem permissão para salvar o arquivo: " + e.Message);
            }
            catch(JsonException e)
            {
                throw new DomainException("Erro na serialização: " + e.Message);
            }

        }
    }
}
