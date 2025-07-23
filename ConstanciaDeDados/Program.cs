using System;
using System.Text.Json;
using ConstanciaDeDados.Entities;
using ConstanciaDeDados.Exceptions;
using ConstanciaDeDados.Services;

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

                    Console.WriteLine("Pessoas já cadastradas");
                    if (pessoas.Count == 0)
                    {
                        Console.WriteLine("Nenhuma pessoas foi cadastrada ainda");
                    }
                    else
                    {
                        foreach (Pessoa p in pessoas)
                        {
                            Console.WriteLine("Nome: "
                                + p.Nome
                                + "\nIdade: "
                                + p.Idade);
                        }
                    }

                    Console.WriteLine("Deseja inserir um novo cadastro?");
                    char resp = char.Parse(Console.ReadLine());

                    if (resp == 's')
                    {
                        Console.Write("Insira o nome:");
                        string nome = Console.ReadLine();
                        Console.Write("Insira a idade:");
                        int idade = int.Parse(Console.ReadLine());

                        pessoas.Add(new Pessoa(nome, idade));
                        PessoaService.Salvar(pessoas);
                    }
                    else
                    {
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
            catch (DomainException e)
            {

            }
        }
    }
}
