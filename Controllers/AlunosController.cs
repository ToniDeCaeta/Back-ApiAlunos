using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlunosApi.Models;
using InterfaceAluno.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        public async Task <ActionResult<IAsyncEnumerable<Aluno>>>  GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao Obter Aluno");
            }
        }

        [HttpGet("AlunoPorNome")]
        public async Task <ActionResult<IAsyncEnumerable<Aluno>>>  GetAlunosByNome([FromQuery] string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByNome(nome);
                    if (alunos.Count() ==0)
                    return NotFound ($"Não existem alunos com o nome: {nome}");
                return Ok(alunos);
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }

        [HttpGet("{id:int}", Name = "GetAluno")]
        public async Task <ActionResult<Aluno>>  GetAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                    if(aluno == null)
                    return NotFound ($"Nao existe aluno com o id {id}");
                return Ok(aluno);
            }
            catch
            {
                return BadRequest ("Request Inválido");
            }
        }


        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new {id =aluno.Id}, aluno);
            }
            catch
            {
                return BadRequest ("Request Inválido");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit (int id, [FromBody] Aluno aluno)//FromBody é o corpo do request
        {
            try
            {
                if (aluno.Id == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    return Ok($"Aluno com o id {id}, foi atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Id invalido");
                }
            }
            catch
            {
                return BadRequest ("Request Inválido");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete (int id)//FromBody é o corpo do request
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if (aluno !=null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"O aluno de id : {id} foi excluido com sucesso");
                }
                else
                {
                    return NotFound ($"Aluno com o id {id} nao foi encontrado");
                }
            }
            catch
            {
                return BadRequest ("Request Inválido");
            }
        }


    }
}
