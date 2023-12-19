namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        readonly decimal precoInicial = 0, precoPorHora = 0;
        readonly List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.Write("Digite a placa do veículo para estacionar: ");
            string placa = Console.ReadLine();
            if (veiculos.Exists(x => x.ToUpper() == placa.ToUpper())) Console.WriteLine("Esse veículo já foi cadastrado.");
            else
            {
                veiculos.Add(placa.ToUpper());
                Console.WriteLine("O veículo foi cadastrado.");
            }
        }

        public void RemoverVeiculo()
        {
            if (veiculos.Any())
            {
                Console.Write("Digite a placa do veículo para remover: ");
                string placa = Console.ReadLine();

                // Verifica se o veículo existe
                if (veiculos.Exists(x => x.ToUpper() == placa.ToUpper()))
                {
                    Console.Write("Digite a quantidade de horas que o veículo permaneceu estacionado: ");
                    decimal horas = 0;
                    try
                    {
                        horas = Convert.ToDecimal(Console.ReadLine());
                        while (horas < 0)
                        {
                            Console.WriteLine("Tenha certeza de ter digitado um número maior ou igual a zero.\n");
                            Console.Write("Digite a quantidade de horas que o veículo permaneceu estacionado: ");
                            horas = Convert.ToDecimal(Console.ReadLine());
                        }
                        decimal valorTotal = precoInicial + precoPorHora * horas;
                        veiculos.Remove(placa);
                        Console.WriteLine("O veículo {0} foi removido e o preço total foi de: R$ {1}", placa.ToUpper(), valorTotal);
                    }
                    catch
                    {
                        Console.WriteLine("Um errou ocorreu. Tenha certeza que digitou um número válido.");
                    }
                }
                else Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente.");
            }
            else Console.WriteLine("Não há veículos estacionados.");
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (string placa in veiculos) Console.WriteLine(placa);
            }
            else Console.WriteLine("Não há veículos estacionados.");
        }
    }
}
