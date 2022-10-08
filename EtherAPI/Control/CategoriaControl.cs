using EtherAPI.Models.Produto;
using EtherAPI.Data.MySql;
using Newtonsoft.Json;

namespace EtherAPI.Control
{
  public class CategoriaControl
  {
    Conexao conn = new Conexao();
    public bool Salvar(Object categoriaJson)
    {
      Categoria categoria = new Categoria();
      categoria = JsonConvert.DeserializeObject<Categoria>(categoriaJson.ToString());
      return categoria.Salvar(conn);
    }
    public bool Excluir(int id)
    {
      Categoria c = new Categoria(id, "");
      return c.Excluir(conn);
    }
    public Categoria ObterPorId(int id)
    {
      Categoria c = new Categoria(id, "");
      return c.ObterPorId(conn);
    }
  }
}
