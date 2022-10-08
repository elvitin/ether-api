using EtherAPI.Data.MySql;
using EtherAPI.Repository.Vendedor;
namespace EtherAPI.Models.Vendedor
{
  public class Vendedor
  {
    public int Id { get; set; }
    public string Nome { get; set; }

    public Vendedor(int id, string nome)
    {
      Id = id;
      Nome = nome;
    }

    public Vendedor()
    {

    }

    public bool Salvar(Conexao conn)
    {
      VendedorRepository vr = new VendedorRepository();
      return vr.Salvar(this, conn);
    }

    public bool Excluir(Conexao conn)
    {
      VendedorRepository vr = new VendedorRepository();
      return vr.Excluir(this.Id, conn);
    }

    public Vendedor ObterPorId(Conexao conn)
    {
      VendedorRepository vr = new VendedorRepository();
      return vr.ObterPorId(this.Id, conn);
    }

    public List<Vendedor> ObterTodos(Conexao conn)
    {
      VendedorRepository vr = new();
      return vr.ObterTodos(conn);
    }

    internal List<Vendedor> ObterPorValor(Conexao conn, string valor)
    {
      VendedorRepository vr = new();
      return vr.ObterPorValor(conn, valor);
    }
  }
}
