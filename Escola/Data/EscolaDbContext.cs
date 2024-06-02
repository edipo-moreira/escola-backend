using Escola.Models;
using Microsoft.EntityFrameworkCore;

namespace Escola.Data
{
    public class EscolaDbContext : DbContext
    {
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<AlunoCurso> AlunoCursos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=Escola.db;Cache=Shared");
        }
    }
}
