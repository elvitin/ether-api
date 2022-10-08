namespace EtherAPI.Models.Venda
{
  public class ItemVenda
  {
    public decimal Qtde { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
    public int ProdutoId { get; set; }
    public int VendaId { get; set; }

    //id da compra

    public ItemVenda()
    {
      this.Qtde = 0;
      this.ValorUnitario = 0;
      this.ValorTotal = 0;
      this.ProdutoId = 0;
      this.VendaId = 0;
    }
  }
}
