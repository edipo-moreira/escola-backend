using Escola.Data;
using Escola.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escola.Controllers
{
    [ApiController]
    [Route("v1")]
    public class AulasController : ControllerBase
    {
        private EscolaDbContext _context;

        public AulasController(EscolaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("aulas")]
        public async Task<IActionResult> ObterAulasAsync()
        {
            var aulas = await _context.Aulas.ToListAsync();
            return Ok(aulas);
        }

        [HttpGet]
        [Route("aulas/{id}")]
        public async Task<IActionResult> ObterAulasPorIdAsync(int id)
        {
            var aula = await _context.Aulas.FirstOrDefaultAsync(e => e.Id == id);
            return aula == null ? NotFound() : Ok(aula);           
        }

        [HttpPost]
        [Route("aulas")]
        public async Task<IActionResult> CriarAulaAsync([FromBody] CreateAula modelo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var aula = new Aula
            {
                Nome = modelo.Nome,
                Descricao = modelo.Descricao,
                FkCursoId = modelo.FkCursoId,
                CriadoEm = DateTime.Now,
            };         

            await _context.AddAsync(aula);
            await _context.SaveChangesAsync();
            return Created($"Aula criado: {aula.Id}", aula);
        }

        [HttpPut]
        [Route("aulas/{id}")]
        public async Task<IActionResult> AtualizarAulaAsync([FromBody] UpdateAula modelo, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var aula = await _context.Aulas.FirstOrDefaultAsync(x => x.Id == id);

            if (aula == null)
                return NotFound();

            aula.Nome = modelo.Nome;
            aula.Descricao = modelo.Descricao;
            aula.FkCursoId = modelo.FkCursoId;
            aula.AtualizadoEm = DateTime.Now;

            _context.Update(aula);
            await _context.SaveChangesAsync();
            return Ok(aula);
        }

        [HttpDelete]
        [Route("aulas/{id}")]
        public async Task<IActionResult> ApagarAulaAsync(int id)
        {
            var aula = await _context.Aulas.FirstOrDefaultAsync(x => x.Id == id);

            if (aula == null)
                NotFound();

            _context.Remove(aula);
            await _context.SaveChangesAsync();
            return Ok("Aula excluido");
        }
    }
}
