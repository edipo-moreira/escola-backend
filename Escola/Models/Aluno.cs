namespace Escola.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }

    public class CreateAluno
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

    }

    public class UpdateAluno
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
    }

}
