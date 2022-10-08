using MySql.Data.MySqlClient;
namespace EtherAPI.Data.MySql
{
  public class Conexao
  {
    public string strConexao;
    public MySqlConnection conn;
    public MySqlCommand cmd;
    MySqlTransaction transacao = null;

    public Conexao()
    {
      Conectar();
    }

    public void Conectar()
    {
      strConexao = Environment.GetEnvironmentVariable("connString");
      conn = new MySqlConnection(strConexao);
      cmd = conn.CreateCommand();
    }
    public void OpenConexao()
    {
      try
      {
        if (conn.State != System.Data.ConnectionState.Open)
          conn.Open();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Erro ao abrir conexão: " + ex.Message);
      }
    }
    public void CloseConexao()
    {
      conn.Close();
    }

    public void StartTrans()
    {
      transacao = conn.BeginTransaction();
    }

    public void CommitTrans()
    {
      if (transacao != null)
      {
        transacao.Commit();
      }
    }

    public void RollBackTrans()
    {
      transacao.Rollback();
    }
  }
}
