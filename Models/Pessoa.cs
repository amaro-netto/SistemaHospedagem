namespace SistemaHospedagem.Models
{
    public class Pessoa
    {
        // Se usarmos o construtor vazio, as propriedades (string) não serão nulas 
        // mas o compilador quer a certeza de que elas terão valor.
        // A forma mais simples de resolver é adicionar '?' para dizer que elas
        // podem ser nulas se o construtor sem parâmetros for usado.
        public Pessoa() { }

        // Este é um Construtor. Ele é usado para criar um objeto Pessoa.
        public Pessoa(string nome, string sobrenome)
        {
            Nome = nome;
            Sobrenome = sobrenome;
        }

        // Adicionando '?' para evitar o warning
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }

        // Adicionamos um tratamento para o caso de Nome/Sobrenome serem nulos
        public string NomeCompleto => $"{Nome} {Sobrenome}"; 
    }
}