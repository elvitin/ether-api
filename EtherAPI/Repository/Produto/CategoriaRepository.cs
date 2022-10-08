using MySql.Data.MySqlClient;
using EtherAPI.Models.Produto;
using EtherAPI.Data.MySql;

namespace EtherAPI.Repository.Produto
{
  public class CategoriaRepository
  {
    public bool Salvar(Categoria c, Conexao conn)
    {
      bool sucesso = false;
      MySqlCommand cmd = conn.cmd;

      try
      {
        if (c.Id == 0)
        {
          cmd.CommandText = $@"insert into 
                                     Categoria (cat_nome) 
                                     values (@Nome)";
        }
        else
        {
          cmd.CommandText = @$"update Categoria 
                                     set cat_nome = @Nome
                                     where cat_id = @Id";

          cmd.Parameters.AddWithValue("@Id", c.Id);
        }

        cmd.Parameters.AddWithValue("@Nome", c.Nome);
        conn.OpenConexao();
        int linhasAfetadas = cmd.ExecuteNonQuery();
        sucesso = linhasAfetadas > 0;

        if (sucesso)
        {
          if (c.Id == 0)
          {
            c.Id = (int)cmd.LastInsertedId;
          }
        }
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

    public bool Excluir(int id, Conexao conn)
    {
      bool sucesso = false;
      MySqlCommand cmd = conn.cmd;

      try
      {
        cmd.CommandText = @$"delete from Categoria where cat_id = {id}";
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

    public Categoria ObterPorId(int id, Conexao conn)
    {
      MySqlCommand cmd = conn.cmd;
      Categoria categoria = null;

      try
      {
        cmd.CommandText = $@"select * from categoria
                                     where cat_id = {id}";

        conn.OpenConexao();

        var dr = cmd.ExecuteReader();

        if (dr.Read())
        {
          categoria = new Categoria();
          categoria.Id = Convert.ToInt32(dr["cat_id"]);
          categoria.Nome = dr["cat_nome"].ToString();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Erro ao obter categoria: " + ex.Message);
      }
      finally
      {
        conn.CloseConexao();
      }
      return categoria;
    }
  }
}
