
using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using sistema_de_rh_API.Contexts;
using sistema_de_rh_API.DTOs;
using sistema_de_rh_API.Models;
using sistema_de_rh_API.Services;

namespace sistema_de_rh_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioService? _funcionarioService;

        public FuncionarioController(FuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FuncionarioDTO funcionario)
        {
            try
            {
                var novoFuncionario = await _funcionarioService.CriarFuncionario(funcionario);

                return CreatedAtAction(nameof(Create), new { email = funcionario.EmailProfissional }, novoFuncionario);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while saving the Funcionario.", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ObterFuncionarios()
        {
            try
            {
                var funcionarios = _funcionarioService.ObterFuncionarios();

                return Ok(funcionarios);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while saving the Funcionario.", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterFuncionario(int id)
        {
            try
            {
                var funcionario = _funcionarioService.ObterFuncionarioPorId(id);                
                return Ok(funcionario);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the Funcionario.", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] FuncionarioDTO funcionarioDTO, int id)
        {
            try
            {
                var UpdateFuncionario = await _funcionarioService.UpdateFuncionario(funcionarioDTO, id);

                return Ok(UpdateFuncionario);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while saving the Funcionario.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _funcionarioService.DeleteFuncionario(id);

                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while saving the Funcionario.", details = ex.Message });
            }
        }
    }
}