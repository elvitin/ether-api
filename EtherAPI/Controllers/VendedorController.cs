using Microsoft.AspNetCore.Mvc;
using EtherAPI.Control;

namespace EtherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : Controller
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult Gravar(Object vendedorJson)
        {
            VendedorControl vendedorControl = new VendedorControl();
            if (vendedorControl.Salvar(vendedorJson))
            {
                return Ok("Vendedor gravado com sucesso");
            }
            else
                return BadRequest("Erro ao salvar a vendedor");
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult Excluir(int id)
        {
            VendedorControl vendedorControl = new VendedorControl();
            if (vendedorControl.Excluir(id))
            {
                return Ok("Vendedor excluído com sucesso");
            }
            else
                return BadRequest("Erro ao excluir o vendedor");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult ObterPorId(int id)
        {
            VendedorControl vendedorControl = new VendedorControl();
            var vendedor = vendedorControl.ObterPorId(id);
            if (vendedor != null)
                return Ok(vendedor);
            return BadRequest("vendedor não encontrado.");
        }
    }
}
