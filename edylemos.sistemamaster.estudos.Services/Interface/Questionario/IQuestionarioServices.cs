using edylemos.sistemamaster.estudos.Domain.Entidades.Questionarios;

namespace edylemos.sistemamaster.estudos.Services.Interface.Questionario
{
    public interface IQuestionarioServices
    {
        void Registrar(EntidadeQuestionario entidadeQuestionario);
        Task<IEnumerable<EntidadeQuestionario>> ObterQuestionario();
        Task<IEnumerable<EntidadeQuestionario>> ObterQuestionarioPorId(int QuestionarioId);
    }
}
