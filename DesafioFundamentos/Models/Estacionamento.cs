using System.Text.RegularExpressions;

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
            int placaModelo = VerificarPlaca(placa);

            if (placaModelo == 0) Console.WriteLine("Placa inválida. Confirme que a placa esteja no padrão Mercosul.");
            else if (veiculos.Exists(x => x.ToUpper() == placa.ToUpper())) Console.WriteLine("Esse veículo já foi cadastrado.");
            else
            {
                veiculos.Add(placa.ToUpper());
                Console.WriteLine("Padrão de placa detectado: " + (placaModelo == 1 ? "Mercosul antigo" : "Mercosul novo"));
                Console.WriteLine("O veículo foi cadastrado.");
            }
        }

        // confirma se placa é válida e descobre padrão utilizado
        // retorna 0 = inválido
        // retorna 1 = padrão mercosul antigo (XXX0000)
        // retorna 2 = padrão mercosul novo (XXX0X00)
        int VerificarPlaca(string placa)
        {
            if (placa.Length != 7) return 0;

            // primeiros três caracteres são somente letras?
            bool verifica1 = Regex.IsMatch(placa.Substring(0, 3), "^[a-zA-Z]+$");

            // últimos quatro caracteres são somente números?
            bool verifica2 = Regex.IsMatch(placa.Substring(3), "^[0-9]+$");

            // últimos quatro caracteres seguem a ordem do mercosul novo?
            bool verifica3 = Regex.IsMatch(placa.Substring(3, 1), "^[0-9]+$") &&
                          Regex.IsMatch(placa.Substring(4, 1), "^[a-zA-Z]+$") &&
                          Regex.IsMatch(placa.Substring(5), "^[0-9]+$");

            if (verifica1 && verifica2) return 1;
            if (verifica1 && verifica3) return 2;

            return 0;
        }

        public void RemoverVeiculo()
        {
            if (!veiculos.Any()) Console.WriteLine("Não há veículos estacionados.");
            else
            {
                Console.Write("Digite a placa do veículo para remover: ");
                string placa = Console.ReadLine();

                // verifica se o veículo não existe
                if (!veiculos.Exists(x => x.ToUpper() == placa.ToUpper()))
                {
                    Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente.");
                }
                else
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
            }
        }

        public void ListarVeiculos()
        {
            // verifica se não há veículos no estacionamento
            if (!veiculos.Any()) Console.WriteLine("Não há veículos estacionados.");
            else
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (string placa in veiculos) Console.WriteLine(placa);
            }
        }
    }
}
