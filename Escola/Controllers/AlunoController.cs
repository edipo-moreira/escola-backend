using Escola.Data;
using Escola.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escola.Controllers
{
    [ApiController]
    [Route("v1")]
    public class AlunosController : ControllerBase
    {
        private EscolaDbContext _context;

        public AlunosController(EscolaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("alunos")]
        public async Task<IActionResult> ObterAlunosAsync()
        {
            var alunos = await _context.Alunos.ToListAsync();
            return Ok(alunos);
        }

        [HttpGet]
        [Route("alunos/{id}")]
        public async Task<IActionResult> ObterAlunosPorIdAsync(int id)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(e => e.Id == id);
            return aluno == null ? NotFound() : Ok(aluno);           
        }

        [HttpPost]
        [Route("alunos")]
        public async Task<IActionResult> CriarAlunoAsync([FromBody] CreateAluno modelo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var aluno = new Aluno
            {
                Nome = modelo.Nome,
                DataNascimento = modelo.DataNascimento,
                CriadoEm = DateTime.Now,
            };         

            await _context.AddAsync(aluno);
            await _context.SaveChangesAsync();
            return Created($"Aluno criado: {aluno.Id}", aluno);
        }

        [HttpPut]
        [Route("alunos/{id}")]
        public async Task<IActionResult> AtualizarAlunoAsync([FromBody] UpdateAluno modelo, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Id == id);

            if (aluno == null)
                return NotFound();

            aluno.Nome = modelo.Nome;
            aluno.DataNascimento = modelo.DataNascimento;
            aluno.AtualizadoEm = DateTime.Now;

            _context.Update(aluno);
            await _context.SaveChangesAsync();
            return Ok(aluno);
        }

        [HttpDelete]
        [Route("alunos/{id}")]
        public async Task<IActionResult> ApagarAlunoAsync(int id)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Id == id);

            if (aluno == null)
                NotFound();

            _context.Remove(aluno);
            await _context.SaveChangesAsync();
            return Ok("Aluno excluido");
        }
    }
}
