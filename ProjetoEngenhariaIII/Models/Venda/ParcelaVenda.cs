namespace EtherAPI.Models.Venda
{
  public class ParcelaVenda
  {
    public int NumeroParcela { get; set; }
    public decimal ValorParcela { get; set; }
    public DateTime DataVencimento { get; set; }

    public ParcelaVenda()
    {

    }

    public ParcelaVenda(int numeroParcela, decimal valorParcela, DateTime dataVencimento)
    {
      this.ValorParcela = valorParcela;
      this.NumeroParcela = numeroParcela;
      this.DataVencimento = dataVencimento;
    }
  }
}
