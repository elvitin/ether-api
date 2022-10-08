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
      strConexao = "Server=us-cdbr-east-04.cleardb.com;Database=heroku_def03bde5529281;Uid=b2f76844e0e397;Pwd=ed4c9ae9;";
      //strConexao = "Server=mysql05-farm88.kinghost.net; Database=vartechs15; Uid=vartechs15; Pwd=A123456789a1;";
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
