namespace EtherAPI.Models.Venda
{
  public class ParcelamentoPessoaJuridica : ParcelamentoTemplateMethod
  {
    protected override void CalcularValorSeguro()
    {
      _valorSeguro = 0.015m / _valorPrincipal + 1;
    }

    protected override void CalcularValorTaxaAdministrativa()
    {
      _valorTaxaAdministrativa = (_valorPrincipal * 0.01m) + 1.5m;
    }
  }
}
