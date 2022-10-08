using Microsoft.AspNetCore.Mvc;
using EtherAPI.Control;

namespace EtherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadeController : Controller
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult Gravar(Object unidadeJson)
        {
            UnidadeControl unidadeControl = new UnidadeControl();
            if (unidadeControl.Salvar(unidadeJson))
            {
                return Ok(unidadeJson);
            }
            else
                return BadRequest("Erro ao salvar a unidade");            
        }

        [HttpDelete]
        [Route("[action]")]
        public IActionResult Excluir(int id)
        {
            UnidadeControl unidadeControl = new UnidadeControl();
         
            if (unidadeControl.Excluir(id))
            {
                return Ok("Unidade excluída com sucesso");
            }
            else
                return BadRequest("Erro ao excluir a unidade");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult ObterPorId(int id)
        {
            UnidadeControl unidadeControl = new UnidadeControl();
            var unidade = unidadeControl.ObterPorId(id);

            if (unidade != null)
                return Ok(unidade);
            return BadRequest("Unidade não encontrada.");
        }
    }
}
