using System.ComponentModel.DataAnnotations;
using Taskify.API.Models.Validations;

namespace Taskify.API.Models
{
    public class Tasks
    {
        public int Id { get; set; }

        [Required(ErrorMessage="O título é obrigatório")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 50 caracteres")]
        public string? Titulo { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 300 caracteres")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória")]
        [DataType(DataType.Date)]
        [FutureDateValidation(ErrorMessage = "A data de vencimento não pode ser no passado")]
        public DateTime DataVencimento { get; set; }

        [Required(ErrorMessage = "O status da tarefa é obrigatório.")]
        public bool Concluida { get; set; }

        [Required(ErrorMessage = "A prioridade é obrigatória.")]
        [RegularExpression("^(Alta|Média|Baixa)$", ErrorMessage = "A prioridade deve ser \"Alta\" \"Média\" ou \"Baixa\".")]
        public string? Prioridade { get; set; }
    }
}
