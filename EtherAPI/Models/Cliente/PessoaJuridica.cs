namespace EtherAPI.Models.Cliente
{
  public class PessoaJuridica
  {
    public string Cnpj { get; set; }
    public string InscEstadual { get; set; }
    public string InscMunicipal { get; set; }
    public int ClienteId { get; set; }

    public PessoaJuridica()
    {

    }

    public PessoaJuridica(string Cnpj, string InscEstadual, string InscMunicipal, int ClienteId)
    {
      this.Cnpj = Cnpj;
      this.InscEstadual = InscEstadual;
      this.InscMunicipal = InscMunicipal;
      this.ClienteId = ClienteId;
    }
  }
}
