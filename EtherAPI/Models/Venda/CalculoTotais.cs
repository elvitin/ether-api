namespace EtherAPI.Models.Venda
{
  public class CalculoTotais
  {
    private InterfaceStrategyCalculo _calculo; //quero receber qualquer objeto que implemente a interface
                                               //desse modo meu contexto estará fechado para modificação
    public CalculoTotais(InterfaceStrategyCalculo calculo)
    {
      this._calculo = calculo;
    }
    public CalculoTotais()
    {
    }

    public decimal CalculaDesconto(Venda venda)   //Invoco a chamada da primeira estratégia, no caso é a PessoaFísica
    {
      _calculo = new CalculoPessoaFisica();
      return _calculo.AplicarDesconto(venda);
    }

    public decimal CalculaAcrescimo(Venda venda)
    {
      _calculo = new CalculoPessoaFisica();
      return _calculo.AplicarAcrescimo(venda);
    }
  }
}
