namespace Taskify.API.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Concluida { get; set; }
        public string? Prioridade { get; set; }
    }
}
