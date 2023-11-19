using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("\nDigite a placa do veículo para estacionar:");
            string placa = Console.ReadLine();

            if (!EhPlacaValida(placa))
                return;

            if (ExisteVeiculoNoPatio(placa))
            {
                Console.WriteLine($"\nVeículo \"{FormatarPlaca(placa)}\" já existe no pátio!");
                return;
            }

            placa = placa.Replace("-", "").ToUpper();
            veiculos.Add(placa);
            Console.WriteLine($"\nVeículo \"{FormatarPlaca(placa)}\" adicionado!");
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("\nDigite a placa do veículo para remover:");
            string placa = Console.ReadLine();

            if (!EhPlacaValida(placa)) return;

            if (ExisteVeiculoNoPatio(placa))
            {
                placa = placa.Replace("-", "").ToUpper();

                Console.WriteLine("\nDigite a quantidade de horas que o veículo permaneceu estacionado:");

                int horas = Convert.ToInt32(Console.ReadLine());
                decimal valorTotal = precoInicial + (precoPorHora * horas);

                veiculos.Remove(placa);
                Console.WriteLine($"\nO veículo {FormatarPlaca(placa)} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
                Console.WriteLine("\nDesculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
        }

        public void ListarVeiculos()
        {
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:\n");
                foreach (string placa in veiculos)
                    Console.WriteLine(FormatarPlaca(placa));
            }
            else
                Console.WriteLine("\nNão há veículos estacionados no pátio.");
        }

        private bool ExisteVeiculoNoPatio(string placa)
        {
            placa = placa.Replace("-", "").ToUpper();

            return veiculos.Any(x => x == placa);
        }

        private bool EhPlacaValida(string placa)
        {
            string padrao = @"\b[a-z]{3}-?[0-9]([a-j]|[0-9])[0-9]{2}\b";

            if (!Regex.IsMatch(placa, padrao, RegexOptions.IgnoreCase))
                Console.WriteLine("\nDigite uma placa válida!" +
                    "\nPadrões válidos:\n(AAA9999, AAA-9999, AAA9(A-J)99 ou AAA-9(A-J)99)");

            return Regex.IsMatch(placa, padrao, RegexOptions.IgnoreCase);
        }

        private string FormatarPlaca(string placa)
        {
            if (placa.Contains('-'))
                return placa;

            return placa.Insert(3, "-");
        }
    }
}
