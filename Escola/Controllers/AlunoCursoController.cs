using Escola.Data;
using Escola.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escola.Controllers
{
    [ApiController]
    [Route("v1")]
    public class AlunoCursosController : ControllerBase
    {
        private EscolaDbContext _context;

        public AlunoCursosController(EscolaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("alunos/{id}/cursos")]
        public async Task<IActionResult> ObterAlunoCursosAsync(int id)
        {
            var cursos = await (from ac in _context.AlunoCursos
                                join c in _context.Cursos on ac.FkCursoId equals c.Id
                                where ac.FkAlunoId == id
                                select c).ToListAsync();

            return cursos == null || !cursos.Any() ? NotFound() : Ok(cursos);
        }

        [HttpPost]
        [Route("alunos/{id}/cursos/{cursoId}")]
        public async Task<IActionResult> CriarAlunoCursoAsync(int id, int cursoId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var curso = new AlunoCurso
            {
                FkAlunoId = id,
                FkCursoId = cursoId,
                CriadoEm = DateTime.Now,
            };         

            await _context.AddAsync(curso);
            await _context.SaveChangesAsync();
            return Created($"AlunoCurso criado: {curso.Id}", curso);
        }

        [HttpDelete]
        [Route("alunos/{id}/cursos/{cursoId}")]
        public async Task<IActionResult> ApagarAlunoCursoAsync(int id, int cursoId)
        {
            var alunoCurso = await _context.AlunoCursos.FirstOrDefaultAsync(x => x.FkAlunoId == id && x.FkCursoId == cursoId);

            if (alunoCurso == null)
                NotFound();

            _context.Remove(alunoCurso);
            await _context.SaveChangesAsync();
            return Ok("AlunoCurso excluido");
        }
    }
}
