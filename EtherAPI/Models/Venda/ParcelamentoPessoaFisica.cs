namespace EtherAPI.Models.Venda
{
  public class ParcelamentoPessoaFisica : ParcelamentoTemplateMethod
  {
    protected override void CalcularValorSeguro()
    {
      _valorSeguro = _valorPrincipal * 0.015m;
    }

    protected override void CalcularValorTaxaAdministrativa()
    {
      _valorTaxaAdministrativa = _valorPrincipal * 0.01m;
    }
  }
}
