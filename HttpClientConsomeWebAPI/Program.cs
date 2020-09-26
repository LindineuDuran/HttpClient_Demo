using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientConsomeWebAPI
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
                //Conecta-se ao webservice
                cliente.BaseAddress = new Uri("https://localhost:44334/");
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Exibe lista de Produtos
                await ExibeListaProdutos(cliente);

                //Exibe um Produto
                HttpResponseMessage response = await ExibeProduto(cliente, 1);

                // HTTP POST - define o produto
                var produto_ipad = new Produto() { Nome = "IPad", Preco = 1999, Categoria = "Tablet" };
                response = await cliente.PostAsJsonAsync("produtos", produto_ipad);

                //Exibe lista de Produtos
                await ExibeListaProdutos(cliente);

                if (response.IsSuccessStatusCode)
                {
                    Uri produtoUrl = response.Headers.Location;
                    // HTTP PUT
                    produto_ipad.Preco = 1800;   // atualiza o preco do produto
                    response = await cliente.PutAsJsonAsync(produtoUrl, produto_ipad);

                    //Exibe lista de Produtos
                    await ExibeListaProdutos(cliente);

                    // HTTP DELETE - deleta o produto
                    response = await cliente.DeleteAsync(produtoUrl);

                    //Exibe lista de Produtos
                    await ExibeListaProdutos(cliente);
                }
            }
        }

        private static async Task<HttpResponseMessage> ExibeProduto(HttpClient cliente, int id)
        {
            // HTTP GET
            HttpResponseMessage response = await cliente.GetAsync("produtos/"+id);
            if (response.IsSuccessStatusCode)
            {
                Produto produto = await response.Content.ReadAsAsync<Produto>();

                Console.WriteLine("{0}\t${1}\t{2}", produto.Nome, produto.Preco, produto.Categoria);

                Console.WriteLine("==========================================================");
            }

            return response;
        }

        private static async Task ExibeListaProdutos(HttpClient cliente)
        {
            // HTTP GET
            HttpResponseMessage responseTest = await cliente.GetAsync("produtos");
            if (responseTest.IsSuccessStatusCode)
            {
                List<ProdutoOutputModel> produtos = await responseTest.Content.ReadAsAsync<List<ProdutoOutputModel>>();

                foreach (ProdutoOutputModel prod in produtos)
                {
                    Console.WriteLine("{0}\t${1}\t{2}", prod.Nome, prod.Preco, prod.Categoria);
                }

                Console.WriteLine("==========================================================");
            }
        }
    }
}
