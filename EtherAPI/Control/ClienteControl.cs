using EtherAPI.Data.MySql;
using EtherAPI.Models.Cliente;
using Newtonsoft.Json;

namespace EtherAPI.Control
{
  public class ClienteControl
  {
    Conexao conn = new Conexao();

    public bool Excluir(int id)
    {
      Cliente cliente = new Cliente(id, "", "", "", "", null, null);
      return cliente.Excluir(conn);
    }

    public Cliente ObterPorId(int id)
    {
      Cliente cliente = new Cliente(id, "", "", "", "", null, null);
      return cliente.ObterPorId(conn);
    }

    public bool Salvar(Object clienteJson)
    {
      Cliente cliente = new Cliente();
      cliente = JsonConvert.DeserializeObject<Cliente>(clienteJson.ToString());
      return cliente.Salvar(conn);
    }

    public string ObterTodos()
    {
      Cliente cliente = new();
      return JsonConvert.SerializeObject(
        cliente.ObterTodos(conn),
        Formatting.Indented,
        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
      );
      //return JsonConvert.SerializeObject(cliente.ObterTodos(conn));
    }

    public string ObterPorValor(string valor)
    {
      Cliente cliente = new();
      return JsonConvert.SerializeObject(
        cliente.ObterPorValor(conn, valor),
        Formatting.Indented,
        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
      );
    }
  }
}
