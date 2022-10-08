using EtherAPI.Models.Vendedor;
using EtherAPI.Data.MySql;
using Newtonsoft.Json;

namespace EtherAPI.Control
{
  public class VendedorControl
  {
    Conexao conn = new Conexao();

    public bool Salvar(Object vendedorJson)
    {
      Vendedor vendedor = new Vendedor();
      vendedor = JsonConvert.DeserializeObject<Vendedor>(vendedorJson.ToString());
      return vendedor.Salvar(conn);
    }

    public bool Excluir(int id)
    {
      Vendedor v = new Vendedor(id, "");
      return v.Excluir(conn);
    }

    public Vendedor ObterPorId(int id)
    {
      Vendedor v = new Vendedor(id, "");
      return v.ObterPorId(conn);
    }

    public string ObterTodos()
    {
      Vendedor vendedor = new();
      //return JsonConvert.SerializeObject(vendedor.ObterTodos(conn), Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
      return JsonConvert.SerializeObject(vendedor.ObterTodos(conn));
    }

    public string ObterPorValor(string valor)
    {
      Vendedor vendedor = new();
      return JsonConvert.SerializeObject(
        vendedor.ObterPorValor(conn, valor),
        Formatting.Indented,
        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
      );
    }
  }
}
