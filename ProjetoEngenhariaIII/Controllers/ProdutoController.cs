using Microsoft.AspNetCore.Mvc;
using EtherAPI.Control;

namespace EtherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult Gravar(Object produtoJson)
        {
            ProdutoControl produtoControl = new ProdutoControl();
            if (produtoControl.Salvar(produtoJson))
            {
                return Ok("Produto gravado com sucesso");
            }
            else
                return BadRequest("Erro ao gravar o produto");
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult Excluir(int id)
        {
            ProdutoControl produtoControl = new ProdutoControl();
            if (produtoControl.Excluir(id))
            {
                return Ok("Produto excluído com sucesso");
            }
            else
                return BadRequest("Erro ao excluir o produto");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult ObterPorId(int id)
        {
            ProdutoControl produtoControl = new ProdutoControl();
            var produto = produtoControl.ObterPorId(id);
            if (produto != null)
                return Ok(produto);
            return BadRequest("Produto não encontrado.");
        }
    }
}

//{
//    "id": 0,
//"nome":"Notebook Helios Predator",
//"preco": 7990.00,
//"estoque": 2,
//"unidadeId": 1,
//"CategoriasIds": [7,21]
//}

//{
//    "id": 0,
//"nome":"IPHONE 14",
//"preco": 12000.00,
//"estoque": 2,
//"unidade":
//{
//        "id": 1,"nome": "PC"
//},
//"categoria":
//{
//        "id": 1,"nome": "cat1"
//}
//}