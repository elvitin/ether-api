namespace EtherAPI.Models.Cliente
{
  public class PessoaFisica
  {
    public string Cpf { get; set; }
    public string Rg { get; set; }
    public string Sexo { get; set; }
    public int ClienteId { get; set; }

    public PessoaFisica()
    {
    }

    public PessoaFisica(string cpf, string rg, string sexo, int clienteId)
    {
      Cpf = cpf;
      Rg = rg;
      Sexo = sexo;
      ClienteId = clienteId;
    }
  }
}
