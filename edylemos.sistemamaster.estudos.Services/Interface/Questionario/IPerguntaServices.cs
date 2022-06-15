using edylemos.sistemamaster.estudos.Domain.Entidades.Questionarios;

namespace edylemos.sistemamaster.estudos.Services.Interface.Questionario
{
    public interface IPerguntaServices
    {
        void Registrar(EntidadePerguntas entidadePergunta);
        Task<IEnumerable<EntidadePerguntas>> ObterPerguntas();
        Task<IEnumerable<EntidadePerguntas>> ObterPerguntasPorId(int PerguntaId);
    }
}
