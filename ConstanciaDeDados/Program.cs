using System;
using System.Text.Json;
using ConstanciaDeDados.Entities;
using ConstanciaDeDados.Exceptions;
using ConstanciaDeDados.Services;
using ConstanciaDeDados.Entities.Enums;

namespace ConstanciaDeDados
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pessoa> pessoas = PessoaService.Carregar();


            try
            {
                while (true)
                {
                    Console.WriteLine("-------------------");
                    Console.WriteLine("Selecione uma opção");
                    Console.WriteLine("-------------------");
                    Console.WriteLine("|Adicionar - [0]  |");
                    Console.WriteLine("|Remover   - [1]  |");
                    Console.WriteLine("|Mostrar   - [2]  |");
                    Console.WriteLine("|Sair      - [3]  |");//adiconar atualizar cadastro
                    Console.WriteLine("-------------------");
                    Console.Write(":");
                    string entrada = (Console.ReadLine());

                    bool parseOk = Enum.TryParse<MenuOpcao>(entrada, out MenuOpcao opcao);

                    switch (opcao)
                    {
                        case MenuOpcao.Adicionar:
                            Console.Write("Insira o nome:");
                            string nome = Console.ReadLine();
                            Console.Write("Insira a idade:");
                            int idade = int.Parse(Console.ReadLine());
                            Console.Write("Insira o ID:");
                            int id = int.Parse(Console.ReadLine());

                            pessoas.Add(new Pessoa(nome, idade, id));
                            PessoaService.Salvar(pessoas);
                            break;

                        case MenuOpcao.Remover:
                            Console.WriteLine("Informe o ID para remover:");
                            int idRemovido = int.Parse(Console.ReadLine());
                            try
                            {
                                PessoaService.Remover(idRemovido);
                                Console.WriteLine($"Pessoa '{idRemovido}' removido(a) com sucesso!!");
                            }
                            catch (DomainException e)
                            {
                                Console.WriteLine("O inserido não existe!" + e.Message);
                            }
                            break;

                        case MenuOpcao.Mostrar:
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
                                        +"\nID: "
                                        + p.Id);   
                                }
                            }
                            break;

                        case MenuOpcao.Sair:
                            
                            Console.WriteLine("Saindo...");
                            return;

                        default:
                            Console.WriteLine("Opção inválida");
                            break;
                    }
                    /*Console.WriteLine();

                    foreach (Pessoa p in pessoas)
                    {
                        Console.WriteLine("Nome: "
                            + p.Nome
                            + "\nIdade: "
                            + p.Idade);
                    }
                    var json = JsonSerializer.Serialize(pessoas);
                    Console.WriteLine(json);

                    Console.WriteLine();

                    List<Pessoa> pessoaDesserializada = JsonSerializer.Deserialize<List<Pessoa>>(json);
                    foreach (Pessoa p in pessoaDesserializada)
                    {
                        Console.WriteLine("Nome: "
                                + p.Nome
                                + "\nIdade: "
                                + p.Idade);
                    }
                    */
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro inesperado!" + e.Message);
            }
        }
    }
}
