namespace EtherAPI.Models.Venda
{
  public abstract class ParcelamentoTemplateMethod : Inutil
  {
    protected decimal _valorPrincipal;
    protected decimal _valorTaxaAdministrativa;
    protected decimal _valorSeguro;

    public ParcelamentoTemplateMethod()
    {

    }

    //esqueleto do template method (operações)

    public override sealed decimal Calcular(decimal valorTotal, int numeroParcelas)
    {
      CalcularValorPrincipal(valorTotal, numeroParcelas);
      CalcularValorTaxaAdministrativa();
      CalcularValorSeguro();
      return CalcularValorTotal();
    }

    //metodo final que será utilizado por todas classes concretas
    protected virtual void CalcularValorPrincipal(decimal valorTotal, int numeroParcelas)
    {
      _valorPrincipal = valorTotal / numeroParcelas;
    }

    //metodos abstratos
    protected abstract void CalcularValorTaxaAdministrativa();
    protected abstract void CalcularValorSeguro();
    protected virtual decimal CalcularValorTotal()
    {
      return _valorPrincipal + _valorTaxaAdministrativa + _valorSeguro;
    }
  }
}
