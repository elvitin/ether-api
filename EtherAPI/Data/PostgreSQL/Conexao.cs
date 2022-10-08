namespace EtherAPI.Data.PostgreSQL
{
  public class Conexao
  {
    private string _usuario = "";
    private string _senha = "";
    private string _endereco = "";
    private string _porta = "";
    private string _banco = "";

    private string _strConexao = "ID=root;" +
                                 "Password=myPassword;" +
                                 "Host=localhost;" +
                                 "Port=5432;" +
                                 "Database=myDataBase;";

    public Conexao()
    {
      //conectar
    }

    //conectar
    //abrir
    //fechar
    //starttrans
    //commit
    //rollback
  }
}
