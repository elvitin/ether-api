namespace EtherAPI.Models.Venda
{
  public class CalculoPessoaJuridica : InterfaceStrategyCalculo
  {
    public decimal AplicarAcrescimo(Venda venda)
    {
      return venda.Acrescimo = (venda.Total * 4) / 100;
    }

    public decimal AplicarDesconto(Venda venda)
    {
      return venda.Desconto = (venda.Total * 3) / 100;
    }
  }
}
