using EtherAPI.Data.MySql;

namespace EtherAPI.Models.Venda
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Total { get; set; }
        public decimal Desconto { get; set; }
        public decimal Acrescimo { get; set; }
        public Cliente.Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public int VendedorId { get; set; } //public Vendedor.Vendedor Vendedor { get; set; }
        public List<ItemVenda> ItensVenda { get; set; }
        //lista de parcela
        public int NumeroParcelas { get; set; }

        public Venda()
        {
            Cliente = new Cliente.Cliente();
            ItensVenda = new List<ItemVenda>();
            //Vendedor = new Vendedor.Vendedor();
        }
        public Venda(int id, DateTime data, decimal total,decimal desconto, decimal acrescimo,
            Cliente.Cliente cliente, int vendedorId,
            List<ItemVenda> itensVenda, int numeroParcelas)
        {
            Id = id;
            Data = data;
            Total = total;
            Desconto = desconto;
            Acrescimo = acrescimo;
            this.Cliente = cliente;
            VendedorId = vendedorId;
            ItensVenda = itensVenda;
            NumeroParcelas = numeroParcelas;
        }
        public bool Salvar(Conexao conn)
        {
            CalculaTotais();
            CalculoParcelamento();
            //instânciar repositoryVenda e chamar Salvar();
            return true;
        }

        //Template method
        public void CalculoParcelamento()
        {
            if(Cliente.TipoPessoa.Equals('F'))
            {
                ParcelamentoTemplateMethod parcelamentoPessoaFisica = new ParcelamentoPessoaFisica();
                parcelamentoPessoaFisica.Calcular(this.Total, this.NumeroParcelas);
            }
            else
            {
                ParcelamentoTemplateMethod parcelamentoPessoaJuridica = new ParcelamentoPessoaJuridica();
                parcelamentoPessoaJuridica.Calcular(this.Total, this.NumeroParcelas);
            }
        }

        //strategy
        public void CalculaTotais()
        {
            CalculoTotais c = new CalculoTotais();

            this.Desconto = c.CalculaDesconto(this);
            this.Acrescimo = c.CalculaAcrescimo(this);

            if (this.Desconto > 0 && this.Desconto < this.Total)
                this.Total = this.Total - Desconto;

            if (this.Acrescimo > 0)
                this.Total = this.Total + Acrescimo;
        }
    }
}
