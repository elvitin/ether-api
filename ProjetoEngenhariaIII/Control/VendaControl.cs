using EtherAPI.Models.Venda;
using EtherAPI.Data.MySql;
using Newtonsoft.Json;
namespace EtherAPI.Control
{
    public class VendaControl
    {
        Conexao conn = new Conexao();
        public bool Salvar(Object vendaJson)
        {
            Venda venda = new Venda();
            venda = JsonConvert.DeserializeObject<Venda>(vendaJson.ToString());
            return venda.Salvar(conn);
        }
    }
}
