using MySql.Data.MySqlClient;
using EtherAPI.Data.MySql;
using EtherAPI.Models.Produto;

namespace EtherAPI.Repository.Produto
{
  public class UnidadeRepository
  {
    public bool Salvar(Models.Produto.Unidade u, Conexao conn)
    {
      bool sucesso = false;
      MySqlCommand cmd = conn.cmd;

      try
      {
        if (u.Id == 0)
        {
          cmd.CommandText = $@"insert into 
                                     Unidade (Nome) 
                                     values (@Nome)";
        }
        else
        {
          cmd.CommandText = @$"update Unidade 
                                     set Nome = @Nome
                                     where Id = @Id";

          cmd.Parameters.AddWithValue("@Id", u.Id);
        }

        cmd.Parameters.AddWithValue("@Nome", u.Nome);

        conn.OpenConexao();

        int linhasAfetadas = cmd.ExecuteNonQuery();

        sucesso = linhasAfetadas > 0;

        if (sucesso)
        {
          if (u.Id == 0)
          {
            u.Id = (int)cmd.LastInsertedId;
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
        cmd.CommandText = @$"delete from Unidade where id = {id}";
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

    public Models.Produto.Unidade ObterPorId(int id, Conexao conn)
    {
      MySqlCommand cmd = conn.cmd;
      Models.Produto.Unidade unidade = null;

      try
      {
        cmd.CommandText = $@"select * from unidade
                                     where id = {id}";

        conn.OpenConexao();

        var dr = cmd.ExecuteReader();

        if (dr.Read())
        {
          unidade = new Models.Produto.Unidade();
          unidade.Id = Convert.ToInt32(dr["id"]);
          unidade.Nome = dr["nome"].ToString();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Erro ao obter unidade: " + ex.Message);
      }
      finally
      {
        conn.CloseConexao();
      }
      return unidade;
    }

    internal List<Unidade> ObterTodos(Conexao conn)
    {
      List<Unidade> unidades = new();

      try
      {
        MySqlCommand cmd = conn.cmd;
        cmd.CommandText = $@"SELECT * FROM unidade;";
        conn.OpenConexao();
        MySqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
          unidades.Add(new()
          {
            Id = Convert.ToInt32(dr["id"]),
            Nome = dr["nome"].ToString()
          });
        }
      }
      catch (Exception)
      {
        throw;
      }
      finally
      {
        conn.CloseConexao();
      }
      return unidades;
    }

    internal List<Unidade> ObterPorValor(Conexao conn, string valor)
    {
      List<Unidade> unidades = new();
      try
      {
        MySqlCommand cmd = conn.cmd;
        cmd.CommandText = $@"SELECT * FROM unidade WHERE Id = @id OR Nome LIKE @nome;";
        uint id = uint.TryParse(valor, out _) ? Convert.ToUInt32(valor) : 0;
        string nome = valor;
        conn.cmd.Parameters.Add("@id", MySqlDbType.UInt32).Value = id;
        conn.cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome + '%';
        conn.OpenConexao();
        MySqlDataReader dr = cmd.ExecuteReader();


        while (dr.Read())
        {
          unidades.Add(new()
          {
            Id = Convert.ToInt32(dr["id"]),
            Nome = dr["nome"].ToString()
          });
        }
      }
      catch (Exception)
      {
        throw;
      }
      finally
      {
        conn.CloseConexao();
      }
      return unidades;
    }
  }
}
