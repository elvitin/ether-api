namespace EtherAPI.Models.Venda
{
  public class CalculoPessoaFisica : InterfaceStrategyCalculo
  {
    public decimal AplicarAcrescimo(Venda venda)
    {
      if (venda.Cliente.TipoPessoa.Equals("F"))               //Estou validando se essa classe é responsável por executar o método
        return venda.Acrescimo = (venda.Total * 3) / 100;
      else                                                   //Se ela não for responsável, ela chama a próxima e, assim sucessivamente.
      {
        CalculoPessoaJuridica calculoPj = new CalculoPessoaJuridica();
        return calculoPj.AplicarAcrescimo(venda);
      }
    }
    public decimal AplicarDesconto(Venda venda)
    {
      if (venda.Cliente.TipoPessoa.Equals("F"))
        return venda.Desconto = (venda.Total * 5) / 100;
      else
      {
        CalculoPessoaJuridica calculoPj = new CalculoPessoaJuridica();
        return calculoPj.AplicarDesconto(venda);
      }
    }
  }
}
