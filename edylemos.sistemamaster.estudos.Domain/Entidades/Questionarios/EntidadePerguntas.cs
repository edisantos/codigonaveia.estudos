using System.ComponentModel.DataAnnotations.Schema;

namespace edylemos.sistemamaster.estudos.Domain.Entidades.Questionarios
{
    [Table(name: "Perguntas")]
    public class EntidadePerguntas
    {
        public int PerguntaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public string? Pergunta { get; set; }
        public int QuestionarioId { get; set; }
        public int TipoPerguntaId { get; set; }
        public string? QuestionarioTitulo { get; set; }
        public string? RespostaNome { get; set; }
        public List<EntidadeTipoPergunta>? TipoPerguntas { get; set; }
    }
}
