using MySql.Data.MySqlClient;
using EtherAPI.Data.MySql;
using EtherAPI.Models.Produto;

namespace EtherAPI.Repository.Produto
{
  public class ProdutoRepository
  {
    public bool Salvar(Models.Produto.Produto p, Conexao conn)
    {
      bool sucesso = false;
      MySqlCommand cmd = conn.cmd;

      try
      {
        if (p.Id == 0)
        {
          cmd.CommandText = $@"INSERT INTO produto 
                                      (prod_nome, prod_preco, prod_estoque, un_id) 
                                      VALUES (@Nome, @Preco, @Estoque, @UnidadeId)";
        }

        else
        {
          cmd.CommandText = @$"UPDATE produto 
                                      set prod_nome = @Nome, prod_preco = @Preco,
                                      prod_estoque = @Estoque, un_id = @UnidadeId
                                      where prod_id = @Id";

          cmd.Parameters.AddWithValue("@Id", p.Id);
        }
        cmd.Parameters.AddWithValue("@Nome", p.Nome);
        cmd.Parameters.AddWithValue("@Preco", p.Preco);
        cmd.Parameters.AddWithValue("@Estoque", p.Estoque);
        cmd.Parameters.AddWithValue("@UnidadeId", p.Unidade.Id);

        conn.OpenConexao();
        conn.StartTrans();

        int linhasAfetadas = cmd.ExecuteNonQuery();
        sucesso = linhasAfetadas > 0;
        if (sucesso)
        {
          if (p.Id == 0)
          {
            p.Id = (int)cmd.LastInsertedId;
          }
        }
        conn.CommitTrans();
      }
      catch (Exception ex)
      {
        conn.RollBackTrans();
        Console.WriteLine(ex.Message);
      }
      finally
      {
        conn.CloseConexao();
      }
      return sucesso;
    }

    public bool Excluir(int id, Conexao conn)
    {
      bool sucesso = false;
      MySqlCommand cmd = conn.cmd;
      try
      {
        cmd.CommandText = @$"delete from produto where prod_id = {id}";
        conn.OpenConexao();
        int linhasAfetadas = cmd.ExecuteNonQuery();
        sucesso = linhasAfetadas > 0;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
      finally
      {
        conn.CloseConexao();
      }
      return sucesso;
    }

    public Models.Produto.Produto ObterPorId(int id, Conexao conn)
    {
      MySqlCommand cmd = conn.cmd;
      Models.Produto.Produto produto = null;
      try
      {
        cmd.CommandText = $@"SELECT 
                                 prod_id,
                                 prod_nome,
                                 prod_preco,
                                 prod_estoque,
                                 unidade.nome AS un_nome,
                                 unidade.id AS un_id
                             FROM
                                 produto
                                     LEFT JOIN
                                 unidade ON unidade.id = produto.un_id
                             WHERE
                                 prod_id = {id}";

        conn.OpenConexao();
        var dr = cmd.ExecuteReader();

        if (dr.Read())
        {
          produto = new Models.Produto.Produto();

          produto.Id = Convert.ToInt32(dr["prod_id"]);
          produto.Nome = dr["prod_nome"].ToString();
          produto.Preco = Convert.ToDecimal(dr["prod_preco"]);
          produto.Estoque = Convert.ToDecimal(dr["prod_estoque"]);
          produto.Unidade.Id = Convert.ToInt32(dr["un_id"]);
          produto.Unidade.Nome = dr["un_nome"].ToString();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Erro ao obter produto: " + ex.Message);
      }
      finally
      {
        conn.CloseConexao();
      }
      return produto;
    }

    public List<Models.Produto.Produto> ObterTodos(Conexao conn)
    {
      List<Models.Produto.Produto> produtos = new();
      try
      {
        MySqlCommand cmd = conn.cmd;
        cmd.CommandText = $@"SELECT 
                                 prod_id,
                                 prod_nome,
                                 prod_preco,
                                 prod_estoque,
                                 unidade.nome AS un_nome,
                                 unidade.id AS un_id
                             FROM
                                 produto
                                     LEFT JOIN
                                 unidade ON unidade.id = produto.un_id";
        conn.OpenConexao();
        MySqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
          produtos.Add(new()
          {
            Id = Convert.ToInt32(dr["prod_id"]),
            Nome = dr["prod_nome"].ToString(),
            Preco = Convert.ToDecimal(dr["prod_preco"]),
            Estoque = Convert.ToDecimal(dr["prod_estoque"]),
            Unidade = new()
            {
              Id = Convert.ToInt32(dr["un_id"]),
              Nome = dr["un_nome"].ToString()
            }
          });
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("[ERRO] (ProdutoRepository): " + ex.Message);
      }
      finally
      {
        conn.CloseConexao();
      }
      return produtos;
    }

    public List<Models.Produto.Produto> ObterPorValor(Conexao conn, string valor)
    {
      List<Models.Produto.Produto> produtos = new();

      try
      {
        MySqlCommand cmd = conn.cmd;
        cmd.CommandText = $@"SELECT 
                                 prod_id,
                                 prod_nome,
                                 prod_preco,
                                 prod_estoque,
                                 unidade.nome AS un_nome,
                                 unidade.id AS un_id
                             FROM
                                 produto
                                     LEFT JOIN
                                 unidade ON unidade.id = produto.un_id
                             WHERE prod_id = @id OR prod_nome LIKE @nome
                             ORDER BY prod_id";

        uint id = uint.TryParse(valor, out _) ? Convert.ToUInt32(valor) : 0;
        string nome = valor;
        conn.cmd.Parameters.Add("@id", MySqlDbType.UInt32).Value = id;
        conn.cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome + '%';
        conn.OpenConexao();
        MySqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
          produtos.Add(new()
          {
            Id = Convert.ToInt32(dr["prod_id"]),
            Nome = dr["prod_nome"].ToString(),
            Preco = Convert.ToDecimal(dr["prod_preco"]),
            Estoque = Convert.ToDecimal(dr["prod_estoque"]),
            Unidade = new()
            {
              Id = Convert.ToInt32(dr["un_id"]),
              Nome = dr["un_nome"].ToString()
            }
          });
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("[ERRO] (ProdutoRepository): " + ex.Message);
      }
      finally
      {
        conn.CloseConexao();
      }
      return produtos;
    }
  }
}

//foreach (var id in p.Categorias)
//{
//    cmd.CommandText = $@"INSERT INTO categoria_produto(prod_id, cat_id)VALUES(@prod_id, @cat_id);";
//    cmd.Parameters.AddWithValue("@prod_id", p.Id);
//    cmd.Parameters.AddWithValue("@cat_id", p.Categorias.IndexOf(id));
//    cmd.ExecuteNonQuery();
//}

//JSON PARA CHAMAR NO POSTMAN
//{
//    "id": 0,
//    "nome":"Desktop Helios Predator",
//    "preco": 7990.00,
//    "estoque": 2,
//    "unidade":
//    {
//        "id": 1,
//        "nome": "un"
//    }
//}