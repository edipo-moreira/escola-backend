namespace Escola.Models
{
    public class AlunoCurso
    {
        public int Id { get; set; }
        public int FkAlunoId { get; set; }
        public int FkCursoId { get; set; }
        public DateTime CriadoEm { get; set; }
    }
    public class CreateAlunoCurso
    {
        public int FkAlunoId { get; set; }
        public int FkCursoId { get; set; }
    }
}
