using System.Collections.Generic;
using System.Linq;

namespace SistemaHospedagem.Models
{
    public class Reserva
    {
        // Propriedades - adicionamos o '?' (Nullable Reference Type) para evitar os warnings
        public List<Pessoa>? Hospedes { get; set; }
        public Suite? Suite { get; set; }
        public int DiasReservados { get; set; }

        public Reserva() { }

        // Construtor para já iniciar com os dias reservados
        public Reserva(int diasReservados)
        {
            DiasReservados = diasReservados;
        }

        // Método para cadastrar a lista de hóspedes (sem alteração)
        public void CadastrarHospedes(List<Pessoa> hospedes)
        {
            if (Suite != null && hospedes.Count <= Suite.Capacidade)
            {
                Hospedes = hospedes;
            }
            else
            {
                throw new System.Exception("A quantidade de hóspedes excede a capacidade da suíte.");
            }
        }

        // Método para cadastrar a suíte (sem alteração)
        public void CadastrarSuite(Suite suite)
        {
            Suite = suite;
        }

        // Método para obter a quantidade de hóspedes cadastrados (sem alteração)
        public int ObterQuantidadeHospedes()
        {
            return Hospedes?.Count ?? 0;
        }
        
        // NOVO MÉTODO: Calcula o valor da diária SEM aplicar o desconto
        public decimal CalcularValorSemDesconto()
        {
            if (Suite == null)
            {
                throw new System.Exception("Suíte não cadastrada. Não é possível calcular o valor.");
            }
            return DiasReservados * Suite.ValorDiaria;
        }

        // Método principal para calcular o valor da diária (COM desconto, se aplicável)
        public decimal CalcularValorDiaria()
        {
            if (Suite == null)
            {
                throw new System.Exception("Suíte não cadastrada. Não é possível calcular o valor.");
            }
            
            decimal valorTotal = CalcularValorSemDesconto(); // Começa com o valor sem desconto

            // REGRA DE NEGÓCIO: Desconto de 10% para mais de 10 dias
            if (DiasReservados > 10)
            {
                decimal desconto = valorTotal * 0.10M;
                valorTotal -= desconto;
            }

            return valorTotal;
        }
    }
}