namespace SistemaHospedagem.Models
{
    public class Suite
    {
        public Suite() { }

        // Construtor para facilitar a criação de uma Suite
        public Suite(string tipoSuite, int capacidade, decimal valorDiaria)
        {
            TipoSuite = tipoSuite;
            Capacidade = capacidade;
            ValorDiaria = valorDiaria;
        }

        // Propriedades (características) da classe Suite
        public string TipoSuite { get; set; }
        public int Capacidade { get; set; }
        public decimal ValorDiaria { get; set; }
    }
}