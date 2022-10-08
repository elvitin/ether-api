using EtherAPI.Data.MySql;
using EtherAPI.Repository.Produto;

namespace EtherAPI.Models.Produto
{
  public class Categoria
  {
    public int Id { get; set; }
    public string Nome { get; set; }

    public Categoria(int id, string nome)
    {
      Id = id;
      Nome = nome;
    }

    public Categoria()
    {

    }

    public bool Salvar(Conexao conn)
    {
      CategoriaRepository cr = new CategoriaRepository();
      return cr.Salvar(this, conn);
    }
    public bool Excluir(Conexao conn)
    {
      CategoriaRepository cr = new CategoriaRepository();
      return cr.Excluir(this.Id, conn);
    }
    public Categoria ObterPorId(Conexao conn)
    {
      CategoriaRepository cr = new CategoriaRepository();
      return cr.ObterPorId(this.Id, conn);
    }
  }
}
