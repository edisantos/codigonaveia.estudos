using edylemos.sistemamaster.estudos.Domain.Entidades.Questionarios;
using edylemos.sistemamaster.estudos.Services.Interface.Questionario;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace edylemos.sistemamaster.estudos.Services.Repository
{
    public class QuestionarioRepository : IQuestionarioServices
    {
        readonly string _connection;
        private SqlCommand? cmd;
        private SqlDataReader? Dr;
        public QuestionarioRepository(IConfiguration config)
        {
            _connection = config.GetConnectionString("DefaultConnection");
        }
       
        public Task<IEnumerable<EntidadeQuestionario>> ObterQuestionarioPorId(int QuestionarioId)
        {
            throw new NotImplementedException();
        }

        public void Registrar(EntidadeQuestionario entidadeQuestionario)
        {
            try
            {
                string strInsert = string.Format(@"INSERT INTO Questionario VALUES(@QuestionarioTitulo, @DataCriacao, 0);
                                                  SELECT SCOPE_IDENTITY() As QuestinarioId;");

               
                
                using var con = new SqlConnection(_connection);
                con.Open();
               
                using (cmd = new SqlCommand(strInsert, con))
                {
                    
                    cmd.Parameters.AddWithValue("@QuestionarioTitulo", entidadeQuestionario.QuestionarioTitulo);
                    cmd.Parameters.AddWithValue("@DataCriacao", DateTime.Now);
                    entidadeQuestionario.QuestionarioId = Convert.ToInt32(cmd.ExecuteScalar());
                    
                }

                
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EntidadeQuestionario>> ObterQuestionario()
        {
            try
            {
                string strSelect = string.Format(@"SELECT QuestionarioId, QuestionarioTitulo FROM Questionario WHERE Estado = 0");
                List<EntidadeQuestionario> lista = new List<EntidadeQuestionario>();
                using var con = new SqlConnection(_connection);

                if (con.State == ConnectionState.Closed)
                    con.Open();
                using (cmd = new SqlCommand(strSelect, con))
                {
                    
                    using (Dr = await cmd.ExecuteReaderAsync())
                    {
                        EntidadeQuestionario mod = null;

                        while (await Dr.ReadAsync())
                        {
                            mod = new EntidadeQuestionario();
                            mod.QuestionarioId = Convert.ToInt32(Dr[0]);
                            mod.QuestionarioTitulo = Convert.ToString(Dr[1]);
                            lista.Add(mod);
                        }
                        return lista;
                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
