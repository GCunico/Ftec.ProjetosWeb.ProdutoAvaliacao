using Ftec.ProjetosWeb.ProdutoAvaliacao.Dominio.Entidades;
using Ftec.ProjetosWeb.ProdutoAvaliacao.Dominio.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ftec.ProjetosWeb.ProdutoAvaliacao.Persistencia
{
    public class ProdutoAvaliacaoRepositorio : IProdutoAvaliacaoRepositorio
    {
        private string stringConexao;

        public ProdutoAvaliacaoRepositorio(string strConexao)
        {
            stringConexao = strConexao;
        }

        public void Inserir(Dominio.Entidades.ProdutoAvaliacao produtoAval)
        {
            using (var conexao = new NpgsqlConnection(stringConexao))
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        var comando = new NpgsqlCommand();
                        comando.Connection = conexao;
                        comando.Transaction = transacao;

                        comando.CommandText = "INSERT INTO public.produto_avaliacao(id, idproduto, idcliente, nota, descricao) VALUES (@id, @idProduto, @idCliente, @nota, @descricao);";
                        comando.Parameters.AddWithValue("id", produtoAval.Id);
                        comando.Parameters.AddWithValue("idProduto", produtoAval.idProduto);
                        comando.Parameters.AddWithValue("idCliente", produtoAval.idCliente);
                        comando.Parameters.AddWithValue("nota", produtoAval.Nota);
                        comando.Parameters.AddWithValue("descricao", produtoAval.Descricao);
                        comando.ExecuteNonQuery();

                        transacao.Commit();
                    }
                    catch (Exception e)
                    {
                        transacao.Rollback();
                        throw e;
                    }
                }
            }
        }

        public void Alterar(Dominio.Entidades.ProdutoAvaliacao produtoAval)
        {
            using (var conexao = new NpgsqlConnection(stringConexao))
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        var comando = new NpgsqlCommand();
                        comando.Connection = conexao;
                        comando.Transaction = transacao;

                        comando.CommandText = "UPDATE public.produto_avaliacao SET nota=@nota, descricao=@descricao WHERE id = @id;";
                        comando.Parameters.AddWithValue("id", produtoAval.Id.ToString());
                        comando.Parameters.AddWithValue("nota", produtoAval.Nota);
                        comando.Parameters.AddWithValue("descricao", produtoAval.Descricao);
                        comando.ExecuteNonQuery();

                        transacao.Commit();
                    }
                    catch (Exception e)
                    {
                        transacao.Rollback();
                        throw e;
                    }
                }
            }
        }

        public void Excluir(Guid id)
        {
            using (var conexao = new NpgsqlConnection(stringConexao))
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        var comando = new NpgsqlCommand();
                        comando.Connection = conexao;
                        comando.Transaction = transacao;

                        comando.CommandText = "DELETE FROM public.produto_avaliacao WHERE id = @id;";
                        comando.Parameters.AddWithValue("id", id.ToString());
                        comando.ExecuteNonQuery();

                        transacao.Commit();
                    }
                    catch (Exception e)
                    {
                        transacao.Rollback();
                        throw e;
                    }
                }
            }
        }

        public Dominio.Entidades.ProdutoAvaliacao Procurar(Guid id)
        {
            Dominio.Entidades.ProdutoAvaliacao produto = null;

            using (var conexao = new NpgsqlConnection(stringConexao))
            {
                conexao.Open();

                var comando = new NpgsqlCommand();
                comando.Connection = conexao;

                comando.CommandText = "SELECT id, idcliente, idproduto, nota, descricao FROM public.produto_avaliacao WHERE id = @id;";
                comando.Parameters.AddWithValue("id", id.ToString());

                using (var reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        produto = new Dominio.Entidades.ProdutoAvaliacao();
                        produto.Id = Guid.Parse(reader["Id"].ToString());
                        produto.idCliente = Guid.Parse(reader["idcliente"].ToString());
                        produto.idProduto = Guid.Parse(reader["idproduto"].ToString());
                        produto.Nota = Convert.ToInt32(reader["nota"].ToString());
                        produto.Descricao = reader["descricao"].ToString();

                    }
                }
            }

            return produto;
        }

        public List<Dominio.Entidades.ProdutoAvaliacao> ProcurarTodasAvaliacoesByProduto(Guid idProduto)
        {
            List<Dominio.Entidades.ProdutoAvaliacao> list = new List<Dominio.Entidades.ProdutoAvaliacao>();
            using (var conexao = new NpgsqlConnection(stringConexao))
            {
                conexao.Open();

                var comando = new NpgsqlCommand();
                comando.Connection = conexao;
                comando.CommandText = "SELECT id, idcliente, idproduto, nota, descricao FROM public.produto_avaliacao WHERE idproduto = @idProduto";
                comando.Parameters.AddWithValue("idProduto", idProduto.ToString());

                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dominio.Entidades.ProdutoAvaliacao aval = new Dominio.Entidades.ProdutoAvaliacao();
                        aval.Id = Guid.Parse(reader["Id"].ToString());
                        aval.idCliente = Guid.Parse(reader["idcliente"].ToString());
                        aval.idProduto = Guid.Parse(reader["idproduto"].ToString());
                        aval.Nota = Convert.ToInt32(reader["nota"].ToString());
                        aval.Descricao = reader["descricao"].ToString();

                        list.Add(aval);
                    }
                }
            }

            return list;
        }
    }
}
