using System;
using System.Collections.Generic;
using System.Linq; // Adicionado para usar o .Count() e outras funcionalidades
using SistemaHospedagem.Models;

namespace SistemaHospedagem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("        BEM-VINDO AO SISTEMA DE RESERVAS       ");
            Console.WriteLine("-----------------------------------------------");

            // 1. Coletando dados da Suíte
            Console.WriteLine("\n--- Configuração da Suíte ---");

            // Coletando o Tipo
            Console.Write("Digite o tipo da suíte (Ex: Premium, Luxo, Padrão): ");
            string? tipoSuite = Console.ReadLine();

            // Coletando a Capacidade
            Console.Write("Digite a capacidade máxima de hóspedes: ");
            int capacidade = 0;
            // Usamos um loop e TryParse para garantir que o usuário digite um número inteiro
            while (!int.TryParse(Console.ReadLine(), out capacidade) || capacidade <= 0)
            {
                Console.Write("Entrada inválida. Digite um número inteiro maior que zero para a capacidade: ");
            }

            // Coletando o Valor da Diária
            Console.Write("Digite o valor da diária (Ex: 150,50): R$ ");
            decimal valorDiaria = 0M;
            // Usamos um loop e TryParse para garantir que o usuário digite um valor decimal
            while (!decimal.TryParse(Console.ReadLine(), out valorDiaria) || valorDiaria <= 0)
            {
                Console.Write("Entrada inválida. Digite um valor de diária válido e maior que zero: R$ ");
            }

            // Criando o objeto Suíte com os dados do usuário
            Suite suite = new Suite(tipoSuite: tipoSuite!, capacidade: capacidade, valorDiaria: valorDiaria);
            
            Console.WriteLine($"\nSuíte '{suite.TipoSuite}' configurada com sucesso!");


            // 2. Coletando Dias Reservados
            Console.WriteLine("\n--- Período da Reserva ---");
            Console.Write("Digite a quantidade de dias da reserva: ");
            int dias = 0;
            while (!int.TryParse(Console.ReadLine(), out dias) || dias <= 0)
            {
                Console.Write("Entrada inválida. Digite um número inteiro maior que zero para os dias: ");
            }


            // 3. Coletando dados dos Hóspedes
            Console.WriteLine($"\n--- Cadastro de Hóspedes (Máximo: {suite.Capacidade}) ---");
            List<Pessoa> hospedes = new List<Pessoa>();

            Console.Write("Quantos hóspedes serão cadastrados? ");
            int numHospedes = 0;
            while (!int.TryParse(Console.ReadLine(), out numHospedes) || numHospedes <= 0 || numHospedes > suite.Capacidade)
            {
                 Console.Write($"Entrada inválida. Digite um número entre 1 e {suite.Capacidade}: ");
            }
            
            // Loop para coletar o nome de cada hóspede
            for (int i = 0; i < numHospedes; i++)
            {
                Console.WriteLine($"\n--- Hóspede #{i + 1} ---");
                Console.Write("Nome: ");
                string? nome = Console.ReadLine();

                Console.Write("Sobrenome: ");
                string? sobrenome = Console.ReadLine();

                // Criando e adicionando a Pessoa à lista
                hospedes.Add(new Pessoa(nome: nome!, sobrenome: sobrenome!));
            }


            // 4. Criando e Processando a Reserva
            Console.WriteLine("\n--- Processando Reserva ---");
            Reserva reserva = new Reserva(diasReservados: dias);
            reserva.CadastrarSuite(suite);
            
            try
            {
                reserva.CadastrarHospedes(hospedes);

                // 5. Exibindo os Resultados
                Console.WriteLine("\n--- Detalhes e Valor ---");
                Console.WriteLine($"Suíte Reservada: {reserva.Suite!.TipoSuite}");
                Console.WriteLine($"Dias Reservados: {reserva.DiasReservados}");
                Console.WriteLine($"Quantidade de Hóspedes: {reserva.ObterQuantidadeHospedes()}");
                
                Console.WriteLine("\nHóspedes:");
                // Agora o '!' é justificado, pois o programa não teria chegado aqui se fosse nulo.
                foreach (Pessoa h in reserva.Hospedes!) 
                {
                    Console.WriteLine($"- {h.NomeCompleto}");
                }

                decimal valorTotal = reserva.CalcularValorDiaria();

                if (reserva.DiasReservados > 10)
                {
                    Console.WriteLine($"\n** Desconto de 10% Aplicado! (Reserva de {reserva.DiasReservados} dias) **");
                }
                
                Console.WriteLine($"\nVALOR TOTAL DA RESERVA: R$ {valorTotal:F2}");
            }
            catch (Exception ex)
            {
                // Se o usuário tentar cadastrar mais pessoas do que o permitido na suíte.
                Console.WriteLine($"\nFalha na Reserva: {ex.Message}");
            }
        }
    }
}