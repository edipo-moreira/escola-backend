namespace Escola.Models
{
    public class Aula
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int FkCursoId { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }


    public class CreateAula
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int FkCursoId { get; set; }
    }

    public class UpdateAula
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int FkCursoId { get; set; }
    }
}
