using MySql.Data.MySqlClient;
using EtherAPI.Data.MySql;
using EtherAPI.Models.Vendedor;

namespace EtherAPI.Repository.Vendedor
{
  public class VendedorRepository
  {
    public bool Salvar(Models.Vendedor.Vendedor v, Conexao conn)
    {
      bool sucesso = false;
      MySqlCommand cmd = conn.cmd;

      try
      {
        if (v.Id == 0)
        {
          cmd.CommandText = $@"insert into 
                                     vendedor (vend_nome) 
                                     values (@Nome)";
        }
        else
        {
          cmd.CommandText = @$"update vendedor 
                                     set vend_nome = @Nome
                                     where vend_id = @Id";

          cmd.Parameters.AddWithValue("@Id", v.Id);
        }

        cmd.Parameters.AddWithValue("@Nome", v.Nome);

        conn.OpenConexao();

        int linhasAfetadas = cmd.ExecuteNonQuery();

        sucesso = linhasAfetadas > 0;

        if (sucesso)
        {
          if (v.Id == 0)
          {
            v.Id = (int)cmd.LastInsertedId;
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
        cmd.CommandText = @$"delete from vendedor where vend_id = {id}";

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

    public Models.Vendedor.Vendedor ObterPorId(int id, Conexao conn)
    {
      MySqlCommand cmd = conn.cmd;
      Models.Vendedor.Vendedor vendedor = null;

      try
      {
        cmd.CommandText = $@"select * from vendedor
                                     where vend_id = {id}";

        conn.OpenConexao();

        var dr = cmd.ExecuteReader();

        if (dr.Read())
        {
          vendedor = new Models.Vendedor.Vendedor();
          vendedor.Id = Convert.ToInt32(dr["vend_id"]);
          vendedor.Nome = dr["vend_nome"].ToString();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Erro ao obter vendedor: " + ex.Message);
      }
      finally
      {
        conn.CloseConexao();
      }
      return vendedor;
    }

    public List<Models.Vendedor.Vendedor> ObterTodos(Conexao conn)
    {
      List<Models.Vendedor.Vendedor> vendedores = new();

      try
      {
        MySqlCommand cmd = conn.cmd;
        cmd.CommandText = $@"SELECT 
                                 `vendedor`.`vend_id`,
                                 `vendedor`.`vend_nome`,
                                 `vendedor`.`vend_cpf`
                             FROM
                                 `vendedor`;";

        conn.OpenConexao();
        MySqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
          Models.Vendedor.Vendedor vendedor = new()
          {
            Id = Convert.ToInt32(dr["vend_id"]),
            Nome = dr["vend_nome"].ToString() + ""
          };
          vendedores.Add(vendedor);
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
      return vendedores;
    }

    internal List<Models.Vendedor.Vendedor> ObterPorValor(Conexao conn, string valor)
    {
      List<Models.Vendedor.Vendedor> vendedores = new();
      try
      {
        MySqlCommand cmd = conn.cmd;
        cmd.CommandText = $@"SELECT 
                                 vend_id, vend_nome
                             FROM
                                 vendedor
                             WHERE
                                 vend_id = @id OR vend_nome LIKE @nome
                             ORDER BY vend_id DESC;";

        uint id = uint.TryParse(valor, out _) ? Convert.ToUInt32(valor) : 0;
        string nome = valor;
        conn.cmd.Parameters.Add("@id", MySqlDbType.UInt32).Value = id;
        conn.cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome + '%';
        conn.OpenConexao();
        MySqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
          vendedores.Add(new()
          {
            Id = Convert.ToInt32(dr["vend_id"]),
            Nome = dr["vend_nome"].ToString() + ""
          });
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("[ERRO] (ClienteRepository): " + ex.Message);
        throw;
      }
      finally
      {
        conn.CloseConexao();
      }
      return vendedores;
    }
  }
}
