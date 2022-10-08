using EtherAPI.Data.MySql;
using EtherAPI.Models.Produto;
using Newtonsoft.Json;
using EtherAPI.Models.Cliente;

namespace EtherAPI.Control
{
  public class ProdutoControl
  {
    Conexao conn = new Conexao();

    public bool Excluir(int id)
    {
      Produto p = new Produto(id, "", 0, 0, null, null);
      return p.Excluir(conn);
    }

    public Produto ObterPorId(int id)
    {
      Produto p = new Produto(id, "", 0, 0, null, null);
      return p.ObterPorId(conn);
    }

    public bool Salvar(Object produtoJson)
    {
      Produto produto = new Produto();
      produto = JsonConvert.DeserializeObject<Produto>(produtoJson.ToString());
      return produto.Salvar(conn);
    }

    public string ObterTodos()
    {
      Produto produto = new();
      return JsonConvert.SerializeObject(produto.ObterTodos(conn));
    }

    public string ObterPorValor(string valor)
    {
      Produto produto = new();
      return JsonConvert.SerializeObject(
        produto.ObterPorValor(conn, valor),
        Formatting.Indented,
        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
      );
    }
  }
}