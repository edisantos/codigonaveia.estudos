using edylemos.sistemamaster.estudos.Domain.Entidades;
using edylemos.sistemamaster.estudos.Services.Interface;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace edylemos.sistemamaster.estudos.Services.Repository
{
    public class UsuarioRepository : IUsuario
    {
        readonly string _connection;
        private SqlCommand? cmd;
        private SqlDataReader? Dr;
        public UsuarioRepository(IConfiguration config)
        {
            _connection = config.GetConnectionString("DefaultConnection");
        }

        public void Registrar(Usuarios usuarios)
        {
            try
            {
                using var con = new SqlConnection(_connection);
                con.Open();
                using (cmd = new SqlCommand("RegistraUsuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PrimeiroNome", usuarios.PrimeiroNome);
                    cmd.Parameters.AddWithValue("@SegundoNome", usuarios.SegundoNome);
                    cmd.Parameters.AddWithValue("@Email", usuarios.Email);
                    cmd.Parameters.AddWithValue("@Usuario", usuarios.Usuario);
                    cmd.Parameters.AddWithValue("@Senha", usuarios.Senha);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Usuarios> ObterTodosUsuario()
        {
            using var con = new SqlConnection(_connection);
            List<Usuarios> lista = new List<Usuarios>();
            con.Open();
            using (cmd = new SqlCommand("ObterTodosUsuarios", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (Dr = cmd.ExecuteReader())
                {
                    Usuarios mod = null;
                    while (Dr.Read())
                    {
                        mod = new Usuarios();
                        mod.Id = Convert.ToInt32(Dr[0]);
                        mod.PrimeiroNome = Convert.ToString(Dr[1]);
                        mod.SegundoNome = Convert.ToString(Dr[2]);
                        mod.Email = Convert.ToString(Dr[3]);
                        mod.Usuario = Convert.ToString(Dr[4]);
                        lista.Add(mod);
                    }
                    return lista;
                }
            }
        }

        public void Excluir(int Id)
        {
            using var con = new SqlConnection(_connection);
            con.Open();
            using(cmd = new SqlCommand("SpRemoverUsuario", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Alterar(Usuarios usuarios)
        {
            using var con = new SqlConnection(_connection);
            con.Open();
            using(cmd = new SqlCommand("SpAlterarUsuario", con)){
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id",usuarios.Id);
                cmd.Parameters.AddWithValue("@PrimeiroNome", usuarios.PrimeiroNome);
                cmd.Parameters.AddWithValue("@SegundoNome", usuarios.SegundoNome);
                cmd.Parameters.AddWithValue("@Email",usuarios.Email);
                cmd.Parameters.AddWithValue("@Usuario",usuarios.Usuario);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Usuarios ObterUsuarioPorId(int Id)
        {
            using var con = new SqlConnection(_connection);
            con.Open();
            using(cmd = new SqlCommand("SpObterUsuarioPorId", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", Id);
                using(Dr = cmd.ExecuteReader())
                {
                    Usuarios mod = null;
                    while (Dr.Read())
                    {
                        mod = new Usuarios();
                        mod.Id = Convert.ToInt32(Dr[0]);
                        mod.PrimeiroNome = Convert.ToString(Dr[1]);
                        mod.SegundoNome = Convert.ToString(Dr[2]);
                        mod.Email = Convert.ToString(Dr[3]);
                        mod.Usuario = Convert.ToString(Dr[4]);

                    }
                    return mod;
                }
            }
        }
    }
}

