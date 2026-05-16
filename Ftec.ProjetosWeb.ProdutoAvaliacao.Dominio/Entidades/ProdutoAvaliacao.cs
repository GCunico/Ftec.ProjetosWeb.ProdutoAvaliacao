using System;
using System.Collections.Generic;
using System.Text;

namespace Ftec.ProjetosWeb.ProdutoAvaliacao.Dominio.Entidades
{
    public class ProdutoAvaliacao : Base
    {
        public Guid idCliente { get; set; }
        public Guid idProduto { get; set; }
        public int Nota { get; set; }
        public string Descricao { get; set; }

        public ProdutoAvaliacao()
        {
            Id = Guid.NewGuid();
        }
    }
}
