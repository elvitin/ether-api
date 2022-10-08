using MySql.Data.MySqlClient;
using EtherAPI.Data.MySql;

namespace EtherAPI.Repository.Cliente
{
  public class ClienteRepository
  {
    public bool Salvar(Models.Cliente.Cliente c, Conexao conn)
    {
      bool sucesso = false;
      int linhasAfetadas;
      MySqlCommand cmd = conn.cmd;

      try
      {
        if (c.Id == 0)
        {
          conn.OpenConexao();
          conn.StartTrans();
          //insert no cliente
          cmd.CommandText = $@"insert into cliente 
                                      (cli_nome, cli_email, cli_fone, cli_tipo_pessoa) 
                                      values (@Nome, @Email, @Fone, @TipoPessoa)";

          cmd.Parameters.AddWithValue("@Nome", c.Nome);
          cmd.Parameters.AddWithValue("@Email", c.Email);
          cmd.Parameters.AddWithValue("@Fone", c.Fone);
          cmd.Parameters.AddWithValue("@TipoPessoa", c.TipoPessoa);

          linhasAfetadas = cmd.ExecuteNonQuery();
          sucesso = linhasAfetadas > 0;
          if (sucesso)                                //se inseriu no cliente, vou inserir na pessoa(fís. ou jur.)
          {
            if (c.Id == 0)
            {
              c.Id = (int)cmd.LastInsertedId;
              //insert no tipo de pessoa
              if (c.TipoPessoa.Equals("F"))
              {
                cmd.CommandText = $@"insert into pessoa_fisica
                                              (pf_cpf, pf_rg, pf_sexo, cli_codigo) 
                                              values (@Cpf, @Rg, @Sexo, @CliCodigo)";

                cmd.Parameters.AddWithValue("@Cpf", c.Fisica.Cpf);
                cmd.Parameters.AddWithValue("@Rg", c.Fisica.Rg);
                cmd.Parameters.AddWithValue("@Sexo", c.Fisica.Sexo);
                cmd.Parameters.AddWithValue("@CliCodigo", c.Id);
                cmd.ExecuteNonQuery();
              }
              else
              {
                cmd.CommandText = $@"insert into pessoa_juridica
                                              (pj_cnpj, pj_insc_estadual, pj_insc_municipal, cli_codigo) 
                                              values (@Cnpj, @InscEstadual, @InscMunicipal, @CliCodigo)";

                cmd.Parameters.AddWithValue("@Cnpj", c.Juridica.Cnpj);
                cmd.Parameters.AddWithValue("@InscEstadual", c.Juridica.InscEstadual);
                cmd.Parameters.AddWithValue("@InscMunicipal", c.Juridica.InscMunicipal);
                cmd.Parameters.AddWithValue("@CliCodigo", c.Id);
                cmd.ExecuteNonQuery();
              }
            }
          }
          conn.CommitTrans();
        }
        else
        {
          conn.OpenConexao();
          conn.StartTrans();
          cmd.CommandText = $@"update cliente set cli_nome = @Nome, cli_email = @Email,
                                      cli_fone = @Fone, cli_tipo_pessoa = @TipoPessoa
                                      where cli_codigo = @CliCodigo";

          cmd.Parameters.AddWithValue("@Nome", c.Nome);
          cmd.Parameters.AddWithValue("@Email", c.Email);
          cmd.Parameters.AddWithValue("@Fone", c.Fone);
          cmd.Parameters.AddWithValue("@TipoPessoa", c.TipoPessoa);
          cmd.Parameters.AddWithValue("@CliCodigo", c.Id);
          linhasAfetadas = cmd.ExecuteNonQuery();
          sucesso = linhasAfetadas > 0;

          if (sucesso)
          {
            if (c.TipoPessoa.Equals("F"))
            {
              cmd.CommandText = $@"update pessoa_fisica set
                                          pf_cpf = @Cpf, pf_rg = @Rg, pf_sexo = @Sexo
                                          where cli_codigo = @ClienteId";

              cmd.Parameters.AddWithValue("@Cpf", c.Fisica.Cpf);
              cmd.Parameters.AddWithValue("@Rg", c.Fisica.Rg);
              cmd.Parameters.AddWithValue("@Sexo", c.Fisica.Sexo);
              cmd.Parameters.AddWithValue("@ClienteId", c.Id);
              cmd.ExecuteNonQuery();
            }
            else
            {
              cmd.CommandText = $@"update pessoa_juridica set
                                          pj_cnpj = @Cnpj, pj_insc_estadual = @InscEstadual,
                                          pj_insc_municipal = @InscMunicipal
                                          where cli_codigo = @ClienteId";

              cmd.Parameters.AddWithValue("@Cnpj", c.Juridica.Cnpj);
              cmd.Parameters.AddWithValue("@InscEstadual", c.Juridica.InscEstadual);
              cmd.Parameters.AddWithValue("@InscMunicipal", c.Juridica.InscMunicipal);
              cmd.Parameters.AddWithValue("@ClienteId", c.Id);
              cmd.ExecuteNonQuery();
            }
          }
          conn.CommitTrans();
        }
      }
      catch (Exception ex)
      {
        conn.RollBackTrans();
        sucesso = false;
        Console.WriteLine(ex.Message);
      }
      finally
      {
        conn.CloseConexao();
      }
      return sucesso;
    }

    public Models.Cliente.Cliente ObterPorId(int id, Conexao conn)
    {
      MySqlCommand cmd = conn.cmd;
      Models.Cliente.Cliente cliente = null;
      try
      {
        cmd.CommandText = $@"select pf.pf_cpf, pf.pf_rg, pf.pf_sexo,
                                    pj.pj_cnpj, pj.pj_insc_estadual, pj.pj_insc_municipal,
                                    cliente.* 
                                    from cliente
                                    left join pessoa_fisica as pf
                                    on pf.cli_codigo = cliente.cli_codigo
                                    left join pessoa_juridica as pj
                                    on pj.cli_codigo = cliente.cli_codigo
                                    where cliente.cli_codigo = {id}";

        conn.OpenConexao();
        var dr = cmd.ExecuteReader();

        if (dr.Read())
        {
          cliente = new Models.Cliente.Cliente();

          cliente.Id = Convert.ToInt32(dr["cli_codigo"]);
          cliente.Nome = dr["cli_nome"].ToString() + "";
          cliente.Email = dr["cli_email"].ToString() + "";
          cliente.Fone = dr["cli_fone"].ToString() + "";
          cliente.TipoPessoa = dr["cli_tipo_pessoa"].ToString() + "";

          if (cliente.TipoPessoa.Equals("F"))
          {
            cliente.Fisica.Cpf = dr["pf_cpf"].ToString() + "";
            cliente.Fisica.Rg = dr["pf_rg"].ToString() + "";
            cliente.Fisica.Sexo = dr["pf_sexo"].ToString() + "";
            cliente.Fisica.ClienteId = (int)cliente.Id;
          }
          else if (cliente.TipoPessoa.Equals("J"))
          {
            cliente.Juridica.Cnpj = dr["pj_cnpj"].ToString() + "";
            cliente.Juridica.InscEstadual = dr["pj_insc_estadual"].ToString() + "";
            cliente.Juridica.InscMunicipal = dr["pj_insc_municipal"].ToString() + "";
            cliente.Juridica.ClienteId = (int)cliente.Id;
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Erro ao obter cliente: " + ex.Message);
      }
      finally
      {
        conn.CloseConexao();
      }
      return cliente;
    }

    public bool Excluir(int id, Conexao conn)
    {
      bool sucesso = false;
      MySqlCommand cmd = conn.cmd;
      try
      {
        cmd.CommandText = @$"delete from cliente where cli_codigo = {id}"; //Implementar cascade
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

    public List<Models.Cliente.Cliente> ObterTodos(Conexao conn)
    {
      List<Models.Cliente.Cliente> clientes = new();

      try
      {
        MySqlCommand cmd = conn.cmd;
        cmd.CommandText = $@"SELECT 
                                pf.pf_cpf,
                                pf.pf_rg,
                                pf.pf_sexo,
                                pj.pj_cnpj,
                                pj.pj_insc_estadual,
                                pj.pj_insc_municipal,
                                cliente.*
                            FROM
                                cliente
                                    LEFT JOIN
                                pessoa_fisica AS pf ON pf.cli_codigo = cliente.cli_codigo
                                    LEFT JOIN
                                pessoa_juridica AS pj ON pj.cli_codigo = cliente.cli_codigo;";

        conn.OpenConexao();
        MySqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
          Models.Cliente.Cliente cliente = CriarObjetoCliente(dr);
          clientes.Add(cliente);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("[ERRO] (ClienteRepository): " + ex.Message);
      }
      finally
      {
        conn.CloseConexao();
      }
      return clientes;
    }

    internal List<Models.Cliente.Cliente> ObterPorValor(Conexao conn, string valor)
    {
      List<Models.Cliente.Cliente> clientes = new();
      try
      {
        MySqlCommand cmd = conn.cmd;

        cmd.CommandText = $@"SELECT 
                                 pf.pf_cpf,
                                 pf.pf_rg,
                                 pf.pf_sexo,
                                 pj.pj_cnpj,
                                 pj.pj_insc_estadual,
                                 pj.pj_insc_municipal,
                                 cliente.*
                             FROM
                                 cliente
                                     LEFT JOIN
                                 pessoa_fisica AS pf ON pf.cli_codigo = cliente.cli_codigo
                                     LEFT JOIN
                                 pessoa_juridica AS pj ON pj.cli_codigo = cliente.cli_codigo
                             WHERE
                                 cliente.cli_codigo = @id
                                     OR cliente.cli_nome LIKE @nome
                             ORDER BY cliente.cli_codigo DESC;";

        uint id = uint.TryParse(valor, out _) ? Convert.ToUInt32(valor) : 0;
        string nome = valor;
        conn.cmd.Parameters.Add("@id", MySqlDbType.UInt32).Value = id;
        conn.cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome + '%';
        conn.OpenConexao();
        MySqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read())
        {
          Models.Cliente.Cliente cliente = CriarObjetoCliente(dr);
          clientes.Add(cliente);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("[ERRO] (ClienteRepository): " + ex.Message);
      }
      finally
      {
        conn.CloseConexao();
      }
      return clientes;
    }

    private Models.Cliente.Cliente CriarObjetoCliente(MySqlDataReader dr)
    {
      Models.Cliente.Cliente cliente = new()
      {
        Id = Convert.ToInt32(dr["cli_codigo"]),
        Nome = dr["cli_nome"].ToString() + "",
        Email = dr["cli_email"].ToString() + "",
        Fone = dr["cli_fone"].ToString() + "",
        TipoPessoa = dr["cli_tipo_pessoa"].ToString() + ""
      };

      if (cliente.TipoPessoa.Equals("F"))
      {
        cliente.Juridica = null;
        cliente.Fisica.Cpf = dr["pf_cpf"].ToString() + "";
        cliente.Fisica.Rg = dr["pf_rg"].ToString() + "";
        cliente.Fisica.Sexo = dr["pf_sexo"].ToString() + "";
        cliente.Fisica.ClienteId = (int)cliente.Id;
      }
      else if (cliente.TipoPessoa.Equals("J"))
      {
        cliente.Fisica = null;
        cliente.Juridica.Cnpj = dr["pj_cnpj"].ToString() + "";
        cliente.Juridica.InscEstadual = dr["pj_insc_estadual"].ToString() + "";
        cliente.Juridica.InscMunicipal = dr["pj_insc_municipal"].ToString() + "";
        cliente.Juridica.ClienteId = (int)cliente.Id;
      }
      return cliente;
    }
  }
}
