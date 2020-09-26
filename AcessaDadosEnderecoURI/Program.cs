using System;
using System.Net.Http;

namespace AcessaDadosEnderecoURI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Rodar();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async void Rodar()
        {
            try
            {
                HttpClient cliente = new HttpClient();

                string resultado = await cliente.GetStringAsync("http://www.macoratti.net/vbn_jqsm.htm");
                Console.WriteLine(resultado);
            }
            catch
            {
                throw;
            }
        }
    }
}
