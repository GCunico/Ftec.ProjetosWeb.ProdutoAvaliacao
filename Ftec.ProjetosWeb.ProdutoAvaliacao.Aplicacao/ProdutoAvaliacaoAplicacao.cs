using Ftec.ProjetosWeb.ProdutoAvaliacao.Aplicacao.Adapter;
using Ftec.ProjetosWeb.ProdutoAvaliacao.Aplicacao.DTO;
using Ftec.ProjetosWeb.ProdutoAvaliacao.Dominio.Entidades;
using Ftec.ProjetosWeb.ProdutoAvaliacao.Dominio.Interfaces;
using Ftec.ProjetosWeb.ProdutoAvaliacao.Persistencia;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ftec.ProjetosWeb.ProdutoAvaliacao.Aplicacao
{
    public class ProdutoAvaliacaoAplicacao
    {
        IProdutoAvaliacaoRepositorio produtoAvaliacaoRepositorio;

        public ProdutoAvaliacaoAplicacao(string strConexao)
        {
            produtoAvaliacaoRepositorio = new ProdutoAvaliacaoRepositorio(strConexao);
        }

        public void AdicionarProdutoAvaliacao(ProdutoAvaliacaoDTO produtoAval)
        {
            Dominio.Entidades.ProdutoAvaliacao prod = ProdutoAvaliacaoAdapter.ParaEntidade(produtoAval);

            prod.Id = Guid.NewGuid();
            produtoAval.Id = prod.Id;

            if (prod.idCliente == Guid.Empty)
                throw new Exception("Necessário informar o cliente.");

            if (prod.idProduto == Guid.Empty)
                throw new Exception("Necessário informar o produto.");

            if (prod.Nota < 0)
                throw new Exception("A nota do produto não deve ser menor que zero.");

            produtoAvaliacaoRepositorio.Inserir(prod);
        }

        public void AlterarProdutoAvaliacao(ProdutoAvaliacaoDTO produtoAval)
        {
            Dominio.Entidades.ProdutoAvaliacao prod = ProdutoAvaliacaoAdapter.ParaEntidade(produtoAval);

            if (prod.Nota < 0)
                throw new Exception("A nota do produto não deve ser menor que zero.");

            if (prod.Descricao == null)
                throw new Exception("Necessário informar uma descrição para a avaliação.");


            produtoAvaliacaoRepositorio.Alterar(prod);
        }

        public void ExcluirProdutoAvaliacao(Guid id)
        {
            if (id == Guid.Empty)
                throw new Exception("O id da avaliação é obrigatoria.");

            produtoAvaliacaoRepositorio.Excluir(id);
        }

        public ProdutoAvaliacaoDTO ObterProdutoAvaliacaoById(Guid id)
        {
            Dominio.Entidades.ProdutoAvaliacao prod = produtoAvaliacaoRepositorio.Procurar(id);
            if (prod == null)
                throw new Exception("Produto não encontrado.");

            return ProdutoAvaliacaoAdapter.ParaDTO(prod);
        }

        public List<ProdutoAvaliacaoDTO> ObterAvaliacoesByIdProduto(Guid idProduto)
        {
            List<Dominio.Entidades.ProdutoAvaliacao> listAvaliacoes = produtoAvaliacaoRepositorio.ProcurarTodasAvaliacoesByProduto(idProduto);

            List<ProdutoAvaliacaoDTO> listAvaliacoesDTO = new List<ProdutoAvaliacaoDTO>();
            if (listAvaliacoes != null)
            {
                foreach (Dominio.Entidades.ProdutoAvaliacao item in listAvaliacoes)
                {
                    listAvaliacoesDTO.Add(ProdutoAvaliacaoAdapter.ParaDTO(item));
                }
            }

            return listAvaliacoesDTO;
        }


    }
}
