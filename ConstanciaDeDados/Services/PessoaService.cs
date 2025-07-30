using ConstanciaDeDados.Exceptions;
using ConstanciaDeDados.Repositories;

namespace ConstanciaDeDados.Services
{
    public static class PessoaService
    {
        public static void Remover(int id)
        {
            var pessoas = PessoaRepository.Carregar();

            var pessoa = pessoas.Find(p => p.Id == id);

            if (pessoa != null)
            {
                pessoas.Remove(pessoa);
                PessoaRepository.Salvar(pessoas);
                Console.WriteLine($"'{pessoa.Nome}' removido(a) com sucesso");
            }
            else
            {
                throw new DomainException ($"Pessoa '{id}' não econtrada ");
            }
        }
        public static int TotalPessoasCadastradas()
        {
            var pessoas = PessoaRepository.Carregar();

            return pessoas.Count();
        }
        public static void Atualizar(int id, string nomeAtualizado)
        {
            var pessoas = PessoaRepository.Carregar();

            var pessoa = pessoas.Find(p => p.Id == id);

            if(pessoa != null)
            {
                pessoa.Nome = nomeAtualizado;
                PessoaRepository.Salvar(pessoas);
                Console.WriteLine($"'{nomeAtualizado}' atualizado com sucesso!");
            }
            else
            {
                throw new DomainException($"Pessoa '{nomeAtualizado}' não encontrada");
            }

        }
    }
}
