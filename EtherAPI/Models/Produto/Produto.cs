using EtherAPI.Repository.Produto;
using EtherAPI.Data.MySql;

namespace EtherAPI.Models.Produto
{
  public class Produto
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public decimal Estoque { get; set; }
    public Unidade Unidade { get; set; }
    public List<Categoria> Categorias { get; set; }

    public Produto(int id, string nome, decimal preco, decimal estoque,
        List<Categoria> categoria, Unidade unidade)
    {
      this.Id = id;
      this.Nome = nome;
      this.Preco = preco;
      this.Estoque = estoque;
      this.Unidade = unidade;
      this.Categorias = categoria;
    }

    public Produto()
    {
      Unidade = new Unidade();
      Categorias = new List<Categoria>();
    }

    public bool Salvar(Conexao conn)
    {
      ProdutoRepository pr = new ProdutoRepository();
      return pr.Salvar(this, conn);
    }

    public bool Excluir(Conexao conn)
    {
      ProdutoRepository pr = new ProdutoRepository();
      return pr.Excluir(this.Id, conn);
    }

    public Produto ObterPorId(Conexao conn)
    {
      ProdutoRepository pr = new ProdutoRepository();
      return pr.ObterPorId(this.Id, conn);
    }

    public List<Produto> ObterTodos(Conexao conn)
    {
      ProdutoRepository pr = new();
      return pr.ObterTodos(conn);
    }

    public List<Produto> ObterPorValor(Conexao conn, string valor)
    {
      ProdutoRepository pr = new();
      return pr.ObterPorValor(conn, valor);
    }
  }
}