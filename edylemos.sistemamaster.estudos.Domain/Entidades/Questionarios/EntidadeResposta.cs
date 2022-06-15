using System.ComponentModel.DataAnnotations.Schema;

namespace edylemos.sistemamaster.estudos.Domain.Entidades.Questionarios
{
    [Table(name: "Resposta")]
    public class EntidadeResposta
    {
        public int QuestionarioId { get; set; }
        public int RespostaId { get; set; }
        public string? RespostaNome { get; set; }
        public int PerguntaId { get; set; }
    }
}
