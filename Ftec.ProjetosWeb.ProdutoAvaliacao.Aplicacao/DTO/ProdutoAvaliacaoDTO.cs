using System;
using System.Collections.Generic;
using System.Text;

namespace Ftec.ProjetosWeb.ProdutoAvaliacao.Aplicacao.DTO
{
    public class ProdutoAvaliacaoDTO
    {
        public Guid Id { get; set; }
        public Guid idCliente { get; set; }
        public Guid idProduto { get; set; }
        public int Nota { get; set; }
        public string Descricao { get; set; }


    }
}
