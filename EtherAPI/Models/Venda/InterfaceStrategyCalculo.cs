namespace EtherAPI.Models.Venda
{
  public interface InterfaceStrategyCalculo
  {
    public decimal AplicarDesconto(Venda venda);
    public decimal AplicarAcrescimo(Venda venda);
  }
}
