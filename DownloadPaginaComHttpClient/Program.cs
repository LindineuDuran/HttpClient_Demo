using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DownloadPaginaComHttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = new Task(DownloadAssincro);
            t.Start();
            Console.WriteLine("Fazendo o download da página...");
            Console.ReadLine();
        }

        static async void DownloadAssincro()
        {
            // ... Define a página
            string pagina = "http://www.macoratti.net/15/08/vbn5_uwb2.htm";

            // ... Usando HttpClient.
            using (HttpClient cliente = new HttpClient())
            using (HttpResponseMessage resposta = await cliente.GetAsync(pagina))
            using (HttpContent conteudo = resposta.Content)
            {
                // ... Lendo a string
                string resultado = await conteudo.ReadAsStringAsync();

                // ... Exibe o resutlado
                if (resultado != null &&
                resultado.Length >= 150)
                {
                    Console.WriteLine(resultado.Substring(0, 150) + "...");
                }
            }
        }
    }
}
