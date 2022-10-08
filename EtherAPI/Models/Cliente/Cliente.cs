using EtherAPI.Data.MySql;
using EtherAPI.Repository.Cliente;

namespace EtherAPI.Models.Cliente
{
  public class Cliente
  {
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Fone { get; set; }
    public string TipoPessoa { get; set; }
    public PessoaJuridica Juridica { get; set; }
    public PessoaFisica Fisica { get; set; }

    public Cliente()
    {
      Juridica = new PessoaJuridica();
      Fisica = new PessoaFisica();
    }

    public Cliente(int id, string nome, string email, string fone, string tipoPessoa, PessoaJuridica juridica, PessoaFisica fisica)
    {
      Id = id;
      Nome = nome;
      Email = email;
      Fone = fone;
      TipoPessoa = tipoPessoa;
      Juridica = juridica;
      Fisica = fisica;
    }

    public bool Salvar(Conexao conn)
    {
      ClienteRepository cr = new ClienteRepository();
      return cr.Salvar(this, conn);
    }

    public bool Excluir(Conexao conn)
    {
      ClienteRepository cr = new ClienteRepository();
      return cr.Excluir(this.Id, conn);
    }

    public Cliente ObterPorId(Conexao conn)
    {
      ClienteRepository cr = new ClienteRepository();
      return cr.ObterPorId(this.Id, conn);
    }

    public List<Cliente> ObterTodos(Conexao conn)
    {
      ClienteRepository cr = new();
      return cr.ObterTodos(conn);
    }

    public List<Cliente> ObterPorValor(Conexao conn, string valor)
    {
      ClienteRepository cr = new();
      return cr.ObterPorValor(conn, valor);
    }
  }
}
