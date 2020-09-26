using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClientConsomeApiVeiculos
{
    class Veiculo
    {
        //public  long Id { get; set; }
        public string Chapa { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }
        public string Descricao { get; set; }
        public bool Vendido { get; set; }

        //public DateTime Created { get; set; }
        //public DateTime Updated { get; set; }

        //public int Decada { get; }

        //public Veiculo(string chapa, string marca, int ano, string descricao, bool vendido)
        //{
        //    Chapa = chapa;
        //    Marca = marca;
        //    Ano = ano;
        //    Descricao = descricao;
        //    Vendido = vendido;
        //}
    }
}
