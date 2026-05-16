using Ftec.ProjetosWeb.ProdutoAvaliacao.Aplicacao.DTO;
using Ftec.ProjetosWeb.ProdutoAvaliacao.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ftec.ProjetosWeb.ProdutoAvaliacao.Aplicacao.Adapter
{
    public static class ProdutoAvaliacaoAdapter
    {
        public static Dominio.Entidades.ProdutoAvaliacao ParaEntidade(ProdutoAvaliacaoDTO pedidoAval)
        {
            Dominio.Entidades.ProdutoAvaliacao ped = new Dominio.Entidades.ProdutoAvaliacao();
            ped.Id = pedidoAval.Id;
            ped.idCliente = pedidoAval.idCliente;
            ped.idProduto = pedidoAval.idProduto;
            ped.Nota = pedidoAval.Nota;
            ped.Descricao = pedidoAval.Descricao;

            return ped;

        }

        public static ProdutoAvaliacaoDTO ParaDTO(Dominio.Entidades.ProdutoAvaliacao pedidoAval)
        {
            ProdutoAvaliacaoDTO ped = new ProdutoAvaliacaoDTO();
            ped.Id = pedidoAval.Id;
            ped.idCliente = pedidoAval.idCliente;
            ped.idProduto = pedidoAval.idProduto;
            ped.Nota = pedidoAval.Nota;
            ped.Descricao = pedidoAval.Descricao;

            return ped;

        }
    }
}
