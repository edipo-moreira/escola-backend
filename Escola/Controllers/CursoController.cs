using Escola.Data;
using Escola.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escola.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CursosController : ControllerBase
    {
        private EscolaDbContext _context;

        public CursosController(EscolaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("cursos")]
        public async Task<IActionResult> ObterCursosAsync()
        {
            var cursos = await _context.Cursos.ToListAsync();
            return Ok(cursos);
        }

        [HttpGet]
        [Route("cursos/{id}")]
        public async Task<IActionResult> ObterCursosPorIdAsync(int id)
        {
            var curso = await _context.Cursos.FirstOrDefaultAsync(e => e.Id == id);
            return curso == null ? NotFound() : Ok(curso);           
        }

        [HttpPost]
        [Route("cursos")]
        public async Task<IActionResult> CriarCursoAsync([FromBody] CreateCurso modelo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var curso = new Curso
            {
                Nome = modelo.Nome,
                Descricao = modelo.Descricao,
                CargaHoraria = modelo.CargaHoraria,
                CriadoEm = DateTime.Now,
            };         

            await _context.AddAsync(curso);
            await _context.SaveChangesAsync();
            return Created($"Curso criado: {curso.Id}", curso);
        }

        [HttpPut]
        [Route("cursos/{id}")]
        public async Task<IActionResult> AtualizarCursoAsync([FromBody] UpdateCurso modelo, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var curso = await _context.Cursos.FirstOrDefaultAsync(x => x.Id == id);

            if (curso == null)
                return NotFound();

            curso.Nome = modelo.Nome;
            curso.Descricao = modelo.Descricao;
            curso.CargaHoraria = modelo.CargaHoraria;
            curso.AtualizadoEm = DateTime.Now;

            _context.Update(curso);
            await _context.SaveChangesAsync();
            return Ok(curso);
        }

        [HttpDelete]
        [Route("cursos/{id}")]
        public async Task<IActionResult> ApagarCursoAsync(int id)
        {
            var curso = await _context.Cursos.FirstOrDefaultAsync(x => x.Id == id);

            if (curso == null)
                NotFound();

            _context.Remove(curso);
            await _context.SaveChangesAsync();
            return Ok("Curso excluido");
        }

        [HttpGet]
        [Route("cursos/{id}/aulas")]
        public async Task<IActionResult> ObterAulasdoCursoPorIdAsync(int id)
        {
            var aulas = await _context.Aulas.Where(e => e.FkCursoId == id).ToListAsync();
            return aulas == null || !aulas.Any() ? NotFound() : Ok(aulas);
        }
    }
}
