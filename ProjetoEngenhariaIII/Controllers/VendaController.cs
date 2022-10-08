using EtherAPI.Control;
using Microsoft.AspNetCore.Mvc;

namespace EtherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult Gravar(Object vendaJson)
        {
            VendaControl vendaControl = new VendaControl();
            if (vendaControl.Salvar(vendaJson))
            {
                return Ok("Venda gravada com sucesso");
            }
            else
                return BadRequest("Erro ao gravar a venda");
        }

    }
}

//JSON GRAVAR VENDA
//{
//    "id": 1,
//"Data":"2022/09/22",
//"Total":400,
//"Desconto":0,
//"Acrescimo":0,
//"NumeroParcelas": 2,
//"Cliente":{ "Id":1,"Nome":"Vinicius","Email":"Vinicius@duesoft.com.br","Fone":"18123456789","TipoPessoa":"F","Juridica":null,"Fisica":null},
//"VendedorId":1,
//"ItensVenda":
//[
//    { "ProdutoId":1, "VendaId":1,"Qtde":1,"ValorUnitario":50,"ValorTotal":50},
//    { "ProdutoId":2, "VendaId":1,"Qtde":1,"ValorUnitario":50,"ValorTotal":50},
//    { "ProdutoId":3, "VendaId":1,"Qtde":4,"ValorUnitario":50,"ValorTotal":200}
//]

//}
