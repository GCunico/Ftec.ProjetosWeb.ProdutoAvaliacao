using Ftec.ProjetosWeb.ProdutoAvaliacao.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ftec.ProjetosWeb.ProdutoAvaliacao.Dominio.Interfaces
{
    public interface IProdutoAvaliacaoRepositorio
    {
        void Inserir(Entidades.ProdutoAvaliacao produtoAval);
        void Alterar(Entidades.ProdutoAvaliacao produtoAval);
        void Excluir(Guid id);
        Entidades.ProdutoAvaliacao Procurar(Guid id);
        List<Entidades.ProdutoAvaliacao> ProcurarTodasAvaliacoesByProduto(Guid idProduto);
    }
}

