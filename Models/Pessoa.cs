namespace SistemaHospedagem.Models
{
    public class Pessoa
    {
        public Pessoa() { }

        // Este é um Construtor. Ele é usado para criar um objeto Pessoa.
        // As propriedades são as características da Pessoa.
        public Pessoa(string nome, string sobrenome)
        {
            Nome = nome;
            Sobrenome = sobrenome;
        }

        // Propriedades (características) da classe Pessoa
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        // Método para exibir o nome completo
        public string NomeCompleto => $"{Nome} {Sobrenome}";
    }
}