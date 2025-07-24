namespace ConstanciaDeDados.Entities
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int Id { get; set; }

        public Pessoa()
        {
        }

        public Pessoa(string nome, int idade, int id)
        {
            Nome = nome;
            Idade = idade;
            Id = id;    
        }
    }
}
