using edylemos.sistemamaster.estudos.Domain.Entidades.Questionarios;

namespace edylemos.sistemamaster.estudos.Services.Interface.Questionario
{
    public interface IRespostaServices
    {
        void Registrar(EntidadeResposta entidateResposta);
        Task<IEnumerable<EntidadePerguntas>> ObterRespostaPorQuestionarioId(int QuestionarioId);
    }
}
