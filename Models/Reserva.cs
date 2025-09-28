using System.Collections.Generic;
using System.Linq;

namespace SistemaHospedagem.Models
{
    public class Reserva
    {
        // Propriedades - conforme o diagrama
        public List<Pessoa> Hospedes { get; set; }
        public Suite Suite { get; set; }
        public int DiasReservados { get; set; }

        public Reserva() { }

        // Construtor para já iniciar com os dias reservados
        public Reserva(int diasReservados)
        {
            DiasReservados = diasReservados;
        }

        // Método para cadastrar a lista de hóspedes
        public void CadastrarHospedes(List<Pessoa> hospedes)
        {
            // Verifica se a quantidade de hóspedes não excede a capacidade da suíte
            if (Suite != null && hospedes.Count <= Suite.Capacidade)
            {
                Hospedes = hospedes;
            }
            else
            {
                // Se exceder, podemos lançar uma exceção (erro) ou apenas avisar
                throw new System.Exception("A quantidade de hóspedes excede a capacidade da suíte.");
            }
        }

        // Método para cadastrar a suíte
        public void CadastrarSuite(Suite suite)
        {
            Suite = suite;
        }

        // Método para obter a quantidade de hóspedes cadastrados
        public int ObterQuantidadeHospedes()
        {
            // Retorna a contagem da lista de hóspedes. Se a lista for nula, retorna 0.
            return Hospedes?.Count ?? 0;
        }

        // Método principal para calcular o valor da diária
        public decimal CalcularValorDiaria()
        {
            // O valor inicial é o valor da diária da suíte vezes a quantidade de dias
            decimal valorTotal = DiasReservados * Suite.ValorDiaria;

            [cite_start]// REGRA DE NEGÓCIO: Se os dias reservados forem MAIORES que 10, aplica 10% de desconto 
            if (DiasReservados > 10)
            {
                decimal desconto = valorTotal * 0.10M; // 10% do valor total
                valorTotal -= desconto; // Subtrai o desconto do valor total
            }

            return valorTotal;
        }
    }
}