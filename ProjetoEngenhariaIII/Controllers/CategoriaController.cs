using Microsoft.AspNetCore.Mvc;
using EtherAPI.Control;

namespace EtherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult Gravar(Object categoriaJson)
        {
            CategoriaControl categoriaControl = new CategoriaControl();
            if (categoriaControl.Salvar(categoriaJson))
            {
                return Ok(categoriaJson);
            }
            else
                return BadRequest("Erro ao salvar a categoria");
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult Excluir(int id)
        {
            CategoriaControl categoriaControl = new CategoriaControl();
            if (categoriaControl.Excluir(id))
            {
                return Ok("Categoria excluída com sucesso");
            }
            else
                return BadRequest("Erro ao excluir a categoria");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult ObterPorId(int id)
        {
            CategoriaControl categoriaControl = new CategoriaControl();
            var categoria = categoriaControl.ObterPorId(id);

            if (categoria != null)
                return Ok(categoria);
            return BadRequest("categoria não encontrada.");
        }
    }
}
