using System.ComponentModel.DataAnnotations.Schema;

namespace edylemos.sistemamaster.estudos.Domain.Entidades.Questionarios
{
    [Table(name:"Questionario")]
    public class EntidadeQuestionario
    {
        public int QuestionarioId { get; set; }
        public string? QuestionarioTitulo { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Estado { get; set; }
    }
}
