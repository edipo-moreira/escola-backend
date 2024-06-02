namespace Escola.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int CargaHoraria { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }

    public class CreateCurso
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int CargaHoraria { get; set; }
    }

    public class UpdateCurso
    {        
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public int CargaHoraria { get; set; }
    }
}
