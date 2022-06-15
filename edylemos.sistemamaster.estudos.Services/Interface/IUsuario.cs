using edylemos.sistemamaster.estudos.Domain.Entidades;

namespace edylemos.sistemamaster.estudos.Services.Interface
{
    public interface IUsuario
    {
        void Registrar(Usuarios usuarios);
        IEnumerable<Usuarios> ObterTodosUsuario();
        void Excluir(int Id);
        void Alterar(Usuarios usuarios);
        Usuarios ObterUsuarioPorId(int Id);
    }
}
