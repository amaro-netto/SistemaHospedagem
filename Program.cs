// Program.cs
using System;
using System.Collections.Generic;
using SistemaHospedagem.Models; // Adiciona o uso das classes da nossa pasta Models

namespace SistemaHospedagem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Para garantir que acentos e símbolos apareçam corretamente

            // 1. Criando os Hóspedes (objetos da classe Pessoa)
            Console.WriteLine("--- Cadastro de Hóspedes ---");
            List<Pessoa> hospedes = new List<Pessoa>();

            Pessoa p1 = new Pessoa(nome: "Pedro", sobrenome: "Silva");
            Pessoa p2 = new Pessoa(nome: "Maria", sobrenome: "Santos");
            Pessoa p3 = new Pessoa(nome: "João", sobrenome: "Ferreira");

            hospedes.Add(p1);
            hospedes.Add(p2);
            // hospedes.Add(p3); // Deixamos um hóspede de fora por enquanto para testar a capacidade

            // 2. Criando a Suíte (objeto da classe Suite)
            Console.WriteLine("\n--- Configuração da Suíte ---");
            Suite suite = new Suite(tipoSuite: "Premium", capacidade: 2, valorDiaria: 100.00M);

            Console.WriteLine($"Suíte: {suite.TipoSuite}");
            Console.WriteLine($"Capacidade Máxima: {suite.Capacidade}");
            Console.WriteLine($"Valor da Diária: R$ {suite.ValorDiaria:F2}");


            // 3. Criando a Reserva (objeto da classe Reserva)
            // Vamos testar com 15 dias para verificar o desconto de 10%
            int dias = 15;
            Reserva reserva = new Reserva(diasReservados: dias);
            reserva.CadastrarSuite(suite);


            // Tentativa de cadastrar os hóspedes na reserva
            try
            {
                reserva.CadastrarHospedes(hospedes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO ao cadastrar hóspedes: {ex.Message}");
                return; // Encerra o programa se der erro na capacidade
            }

            // 4. Exibindo os Resultados
            Console.WriteLine("\n--- Detalhes da Reserva ---");
            Console.WriteLine($"Dias Reservados: {reserva.DiasReservados}");
            Console.WriteLine($"Quantidade de Hóspedes: {reserva.ObterQuantidadeHospedes()}");

            Console.WriteLine("Hóspedes Cadastrados:");
            foreach (Pessoa h in reserva.Hospedes)
            {
                Console.WriteLine($"- {h.NomeCompleto}");
            }

            // Calculando e exibindo o valor total
            decimal valorTotal = reserva.CalcularValorDiaria();

            // Lógica para saber se o desconto foi aplicado
            if (reserva.DiasReservados > 10)
            {
                Console.WriteLine($"\n** Desconto de 10% Aplicado! (Reserva de {reserva.DiasReservados} dias) **");
            }
            else
            {
                Console.WriteLine("\nDesconto não aplicado (Reserva de 10 dias ou menos).");
            }

            Console.WriteLine($"\nVALOR TOTAL DA RESERVA: R$ {valorTotal:F2}");
        }
    }
}