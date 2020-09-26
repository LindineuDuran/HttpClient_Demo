using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientConsomeApiVeiculos
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecutarAsync().Wait();
            Console.ReadLine();
        }

        static async Task ExecutarAsync()
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://localhost:8080/veiculos");
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync("veiculos/1");
                if (response.IsSuccessStatusCode)
                {
                    Veiculo veiculo = await response.Content.ReadAsAsync<Veiculo>();

                    Console.WriteLine("{0}\t${1}", veiculo.Descricao, veiculo.Vendido);
                }

                // HTTP POST - define o produto
                //var veiculoNovo = new Veiculo("GOY8299", "FORD", 1995, "EcoSport", false);

                DateTime dataCorrente = DateTime.Now;
                var veiculoNovo = new { Chapa = "GOY8299", Marca = "FORD", Ano = 1995, Descricao = "EcoSport", Vendido = false };
                //response = await cliente.PostAsJsonAsync("veiculos", veiculoNovo);

                string myJson = "{  'chapa ':  'GOY8299 ',  'marca ':  'FORD ',  'ano ': 1995,  'descricao ':  'Ford EcoSport Ano 1995 ',  'vendido ': false }";
                response = await cliente.PostAsJsonAsync("veiculos", new StringContent(myJson, Encoding.UTF8, "application/json"));

                //var postRequest = new HttpRequestMessage(HttpMethod.Post, "veiculos")
                //{
                //    Content = JsonContent.Create(veiculoNovo)
                //};

                //var postResponse = await cliente.SendAsync(postRequest);

                //postResponse.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    //Veiculo veiculo = await response.Content.ReadAsAsync<Veiculo>();
                    //Console.WriteLine("{0}\t${1}", veiculo.Descricao, veiculo.Vendido);

                    Uri veiculoUrl = response.Headers.Location;

                    //// HTTP PUT
                    //veiculoNovo.Vendido = true;   // atualiza a disponibildade do veículo
                    //response = await cliente.PutAsJsonAsync(veiculoUrl, veiculoNovo);

                    //// HTTP DELETE - deleta o produto
                    //response = await cliente.DeleteAsync(veiculoUrl);
                }
                else
                {
                    Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
                }
            }
        }
    }
}
