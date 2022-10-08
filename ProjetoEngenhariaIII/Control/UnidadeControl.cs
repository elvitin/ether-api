using EtherAPI.Models.Produto;
using EtherAPI.Data.MySql;
using Newtonsoft.Json;

namespace EtherAPI.Control
{
    public class UnidadeControl
    {
        Conexao conn = new Conexao();
        public bool Salvar(Object unidadeJson)
        {
            Unidade unidade = new Unidade();
            unidade = JsonConvert.DeserializeObject<Unidade>(unidadeJson.ToString());
            return unidade.Salvar(conn);
        }
        public bool Excluir(int id)
        {
            Unidade u = new Unidade(id, "");
            return u.Excluir(conn);
        }
        public Unidade ObterPorId(int id)
        {
            Unidade u = new Unidade(id, "");
            return u.ObterPorId(conn);
        }

    }
}
