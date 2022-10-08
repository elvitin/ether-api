using EtherAPI.Data.MySql;
using EtherAPI.Repository.Produto;

namespace EtherAPI.Models.Produto
{
    public class Unidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public Unidade(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    
        public Unidade()
        {
            
        }
        public bool Salvar(Conexao conn)
        {
            UnidadeRepository ur = new UnidadeRepository();            
            return ur.Salvar(this, conn);
        }

        public bool Excluir(Conexao conn)
        {
            UnidadeRepository ur = new UnidadeRepository();
            return ur.Excluir(this.Id, conn);
        }

        public Unidade ObterPorId(Conexao conn)
        {
            UnidadeRepository ur = new UnidadeRepository();
            return ur.ObterPorId(this.Id, conn);
        }
    }
}
