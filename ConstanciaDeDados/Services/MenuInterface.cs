using ConstanciaDeDados.Entities;
using ConstanciaDeDados.Exceptions;
using ConstanciaDeDados.Entities.Enums;
using ConstanciaDeDados.Repositories;

namespace ConstanciaDeDados.Services
{
    public class MenuInterface
    {
        public void MenuExecutar()
        {
            List<Pessoa> pessoas = PessoaRepository.Carregar();

            while (true)
            {
                int total = PessoaService.TotalPessoasCadastradas();
                Console.WriteLine(" _____________________");
                Console.WriteLine("|Selecione uma opção  |");
                Console.WriteLine("|---------------------|");
                Console.WriteLine("|Adicionar - [0]      |");
                Console.WriteLine("|Remover   - [1]      |");
                Console.WriteLine("|Mostrar   - [2]      |");
                Console.WriteLine("|Atualizar - [3]      |");
                Console.WriteLine("|Sair      - [4]      |");
                Console.WriteLine("|---------------------|");
                Console.WriteLine($"|Total Cadastro     {total} |");
                Console.WriteLine(" --------------------");
                Console.Write(":");
                string entrada = (Console.ReadLine());

                bool parseOk = Enum.TryParse<MenuOpcao>(entrada, out MenuOpcao opcao);

                switch (opcao)
                {
                    case MenuOpcao.Adicionar:
                        Console.Clear();
                        Console.Write("Insira o nome:");
                        string nome = Console.ReadLine();
                        Console.Write("Insira a idade:");
                        if (!int.TryParse(Console.ReadLine(), out int idade))
                        {
                            Console.WriteLine("Idade inválida.");
                            break;
                        }
                        Console.Write("Insira o ID:");
                        if (!int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.WriteLine("ID inválido.");
                            break;
                        }
                        if (pessoas.Any(p => p.Id == id))
                        {
                            Console.WriteLine($"Já existe uma pessoa com o ID {id}. Escolha outro.");
                            break;
                        }

                        pessoas.Add(new Pessoa(nome, idade, id));
                        PessoaRepository.Salvar(pessoas);
                        pessoas = PessoaRepository.Carregar();
                        break;

                    case MenuOpcao.Remover:
                        Console.WriteLine("Informe o ID para remover:");
                        int idRemovido = int.Parse(Console.ReadLine());
                        try
                        {
                            Console.Clear();
                            PessoaService.Remover(idRemovido);
                            pessoas = PessoaRepository.Carregar();
                        }
                        catch (DomainException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case MenuOpcao.Mostrar:
                        Console.Clear();
                        if (pessoas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma pessoas foi cadastrada ainda");
                        }
                        else
                        {
                            foreach (Pessoa p in pessoas)
                            {
                                Console.WriteLine("___________________________________________");
                                Console.WriteLine("Nome: "
                                    + p.Nome
                                    + "\nIdade: "
                                    + p.Idade
                                    + "\nID: "
                                    + p.Id);
                            }
                        }
                        break;
                    case MenuOpcao.Atualizar:
                        Console.WriteLine("Informe o ID da pessoa a ser atualizada:");
                        int idAtualizado = int.Parse(Console.ReadLine());

                        try
                        {
                            Console.WriteLine("Insira novo nome:");
                            string nomeAtualizado = Console.ReadLine();

                            PessoaService.Atualizar(idAtualizado, nomeAtualizado);
                            pessoas = PessoaRepository.Carregar();
                        }
                        catch (DomainException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case MenuOpcao.Sair:
                        Console.Clear();
                        Console.Write("Saindo");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(300);
                        Console.Write(".");
                        Thread.Sleep(500);
                        return;

                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
            }
        }
    }

}
    

