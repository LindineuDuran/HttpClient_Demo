using ProdutosWebAPI.Models;
using System.Collections.Generic;

namespace ProdutosWebAPI.Service
{
    public interface IProdutoService
    {
        List<Produto> GetProdutos();
        Produto GetProduto(int id);
        void AddProduto(Produto item);
        void UpdateProduto(Produto item);
        void DeleteProduto(int id);
        bool ProdutoExists(int id);
    }
}
