using EtherAPI.Control;
using Microsoft.AspNetCore.Mvc;

namespace EtherAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ClienteController : ControllerBase
  {
    [HttpPost]
    [Route("[action]")]
    public IActionResult Gravar(Object clienteJson)
    {
      ClienteControl clienteControl = new ClienteControl();
      if (clienteControl.Salvar(clienteJson))
      {
        return Ok("Cliente gravado com sucesso");
      }
      else
        return BadRequest("Erro ao gravar o cliente");
    }

    [HttpDelete]
    [Route("[action]")]
    public IActionResult Excluir(int id)
    {
      ClienteControl clienteControl = new ClienteControl();
      if (clienteControl.Excluir(id))
      {
        return Ok("Cliente excluído com sucesso");
      }
      else
        return BadRequest("Erro ao excluir o cliente");
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult ObterPorId(int id)
    {
      ClienteControl clienteControl = new ClienteControl();
      var cliente = clienteControl.ObterPorId(id);
      if (cliente != null)
        return Ok(cliente);
      return BadRequest("Cliente não encontrado.");
    }
  }
}

/*{
    "id": 0,
    "nome": "Neymar",
    "email": "Neymar@selecao.com.br",
    "fone": "1199877788",
    "tipoPessoa": "F",
    "juridica": {
        "cnpj": "",
        "inscEstadual": "",
        "inscMunicipal": "",
        "clienteId": 0
    },
    "fisica": {
        "cpf": "11111111111",
        "rg": "222222222",
        "sexo": "M",
        "clienteId": 0
    }
}*/