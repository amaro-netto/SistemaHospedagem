using System;
using System.Collections.Generic;
using System.Linq;
using SistemaHospedagem.Models;

namespace SistemaHospedagem
{
    class Program
    {
        // Lista para armazenar todas as reservas feitas (fora do Main)
        static List<Reserva> ListaDeReservas = new List<Reserva>();
        static int ProximoIdReserva = 1; // Contador para dar um ID único a cada reserva

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool continuar = true;
            
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("        BEM-VINDO AO SISTEMA DE RESERVAS       ");
            Console.WriteLine("-----------------------------------------------");

            // Loop principal do menu
            while (continuar)
            {
                ExibirMenu();
                string? escolha = Console.ReadLine();
                Console.WriteLine();

                switch (escolha)
                {
                    case "1":
                        CadastrarNovaReserva();
                        break;
                    case "2":
                        ListarReservas();
                        break;
                    case "3":
                        CancelarReserva();
                        break;
                    case "4":
                        continuar = false;
                        Console.WriteLine("Obrigado por usar o Sistema de Reservas. Até logo!");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Por favor, escolha uma opção entre 1 e 4.");
                        break;
                }
                
                // Pausa para que o usuário veja o resultado antes de o menu limpar
                if (continuar)
                {
                    Console.WriteLine("\n\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear(); // Limpa a tela para o próximo menu
                }
            }
        }

        static void ExibirMenu()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("                 MENU PRINCIPAL                ");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("1 - Cadastrar Nova Reserva");
            Console.WriteLine("2 - Visualizar Todas as Reservas");
            Console.WriteLine("3 - Cancelar Reserva");
            Console.WriteLine("4 - Sair do Sistema");
            Console.Write("\nEscolha uma opção: ");
        }

        // Método que contém toda a lógica de interação que criamos anteriormente
        static void CadastrarNovaReserva()
        {
            Console.Clear();
            Console.WriteLine("--- NOVO CADASTRO DE RESERVA ---");

            // --- 1. Coletando dados da Suíte ---
            // ... [Lógica de coleta de dados da Suíte] ...

            Console.Write("Digite o tipo da suíte (Ex: Premium, Luxo, Padrão): ");
            string? tipoSuite = Console.ReadLine();

            Console.Write("Digite a capacidade máxima de hóspedes: ");
            int capacidade = 0;
            while (!int.TryParse(Console.ReadLine(), out capacidade) || capacidade <= 0)
            {
                Console.Write("Entrada inválida. Digite um número inteiro maior que zero para a capacidade: ");
            }

            Console.Write("Digite o valor da diária (Ex: 150,50): R$ ");
            decimal valorDiaria = 0M;
            while (!decimal.TryParse(Console.ReadLine(), out valorDiaria) || valorDiaria <= 0)
            {
                Console.Write("Entrada inválida. Digite um valor de diária válido e maior que zero: R$ ");
            }

            Suite suite = new Suite(tipoSuite: tipoSuite!, capacidade: capacidade, valorDiaria: valorDiaria);
            Console.WriteLine($"\nSuíte '{suite.TipoSuite}' configurada com sucesso!");


            // --- 2. Coletando Dias Reservados ---
            Console.WriteLine("\n--- Período da Reserva ---");
            Console.Write("Digite a quantidade de dias da reserva: ");
            int dias = 0;
            while (!int.TryParse(Console.ReadLine(), out dias) || dias <= 0)
            {
                Console.Write("Entrada inválida. Digite um número inteiro maior que zero para os dias: ");
            }


            // --- 3. Coletando dados dos Hóspedes ---
            Console.WriteLine($"\n--- Cadastro de Hóspedes (Máximo: {suite.Capacidade}) ---");
            List<Pessoa> hospedes = new List<Pessoa>();

            Console.Write("Quantos hóspedes serão cadastrados? ");
            int numHospedes = 0;
            while (!int.TryParse(Console.ReadLine(), out numHospedes) || numHospedes <= 0 || numHospedes > suite.Capacidade)
            {
                 Console.Write($"Entrada inválida. Digite um número entre 1 e {suite.Capacidade}: ");
            }
            
            for (int i = 0; i < numHospedes; i++)
            {
                Console.WriteLine($"\n--- Hóspede #{i + 1} ---");
                Console.Write("Nome: ");
                string? nome = Console.ReadLine();

                Console.Write("Sobrenome: ");
                string? sobrenome = Console.ReadLine();

                hospedes.Add(new Pessoa(nome: nome!, sobrenome: sobrenome!));
            }


            // --- 4. Criando e Processando a Reserva ---
            Console.WriteLine("\n--- Processando Reserva ---");
            Reserva reserva = new Reserva(diasReservados: dias);
            reserva.CadastrarSuite(suite);
            
            try
            {
                reserva.CadastrarHospedes(hospedes);
                
                // Adiciona a reserva à lista global
                ListaDeReservas.Add(reserva);
                
                // Atribui um ID (simulação)
                // Usaremos a propriedade Hospedes para guardar o ID temporariamente na listagem
                // O ideal seria criar uma propriedade 'Id' na classe Reserva, mas simplificamos para o console.
                reserva.DiasReservados = ProximoIdReserva; 

                // --- 5. Exibindo os Resultados (Com valores de desconto) ---
                decimal valorSemDesconto = reserva.CalcularValorSemDesconto();
                decimal valorTotal = reserva.CalcularValorDiaria();

                Console.WriteLine("\n*** RESERVA REALIZADA COM SUCESSO! ***");
                Console.WriteLine($"ID da Reserva (Para Cancelamento): {reserva.DiasReservados}");
                Console.WriteLine($"Suíte: {reserva.Suite!.TipoSuite} | Dias: {reserva.DiasReservados}");
                
                if (valorTotal < valorSemDesconto)
                {
                    Console.WriteLine($"Valor Bruto (sem desconto): R$ {valorSemDesconto:F2}");
                    Console.WriteLine($"** Valor Final (com 10% de desconto): R$ {valorTotal:F2} **");
                }
                else
                {
                    Console.WriteLine($"Valor Total: R$ {valorTotal:F2}");
                }

                ProximoIdReserva++; // Incrementa para o próximo ID
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFalha na Reserva: {ex.Message}");
            }
        }
        
        static void ListarReservas()
        {
            Console.Clear();
            Console.WriteLine("--- VISUALIZAR TODAS AS RESERVAS ---");
            
            if (ListaDeReservas.Count == 0)
            {
                Console.WriteLine("Nenhuma reserva cadastrada ainda.");
                return;
            }
            
            foreach (var reserva in ListaDeReservas)
            {
                // Para exibir o ID da reserva, usaremos a propriedade DiasReservados, 
                // pois modificamos ela para guardar o ID (ProximoIdReserva).
                Console.WriteLine("------------------------------------------");
                Console.WriteLine($"ID: {reserva.DiasReservados}");
                Console.WriteLine($"Suíte: {reserva.Suite!.TipoSuite} | Capacidade: {reserva.Suite.Capacidade}");
                Console.WriteLine($"Dias: {reserva.DiasReservados} | Hóspedes: {reserva.ObterQuantidadeHospedes()}");
                Console.WriteLine($"Valor Final: R$ {reserva.CalcularValorDiaria():F2}");
                Console.WriteLine($"Hóspede Principal: {reserva.Hospedes?.FirstOrDefault()?.NomeCompleto ?? "N/A"}");
            }
            Console.WriteLine("------------------------------------------");
        }

        static void CancelarReserva()
        {
            Console.Clear();
            Console.WriteLine("--- CANCELAR RESERVA ---");

            if (ListaDeReservas.Count == 0)
            {
                Console.WriteLine("Nenhuma reserva ativa para cancelar.");
                return;
            }

            Console.Write("Digite o ID da reserva que deseja cancelar: ");
            if (int.TryParse(Console.ReadLine(), out int idParaCancelar))
            {
                // Busca a reserva pelo ID (que está armazenado em DiasReservados)
                Reserva? reservaParaRemover = ListaDeReservas.FirstOrDefault(r => r.DiasReservados == idParaCancelar);

                if (reservaParaRemover != null)
                {
                    ListaDeReservas.Remove(reservaParaRemover);
                    Console.WriteLine($"\nReserva ID {idParaCancelar} ({reservaParaRemover.Suite!.TipoSuite}) cancelada com sucesso!");
                }
                else
                {
                    Console.WriteLine($"\nErro: Reserva com ID {idParaCancelar} não encontrada.");
                }
            }
            else
            {
                Console.WriteLine("Erro: ID inválido. Por favor, digite um número.");
            }
        }
    }
}