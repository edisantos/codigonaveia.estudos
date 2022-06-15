using edylemos.sistemamaster.estudos.Domain.Entidades.Questionarios;
using edylemos.sistemamaster.estudos.Services.Interface.Questionario;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace edylemos.sistemamaster.estudos.Services.Repository
{
    public class RespostaRepository : IRespostaServices
    {
        readonly string _connection;
        private SqlCommand? cmd;
        private SqlDataReader? Dr;
        public RespostaRepository(IConfiguration config)
        {
            _connection = config.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<EntidadePerguntas>> ObterRespostaPorQuestionarioId(int QuestionarioId)
        {
            try
            {
                string strSelect = string.Format(@"SELECT Q.QuestionarioId,P.PerguntaId, P.Pergunta,Q.QuestionarioTitulo 
                                                   FROM Perguntas P
                                                   INNER JOIN Questionario Q ON P.QuestionarioId = Q.QuestionarioId
                                                   WHERE Q.QuestionarioId = @QuestionarioId AND Estado = 0");
                List<EntidadePerguntas> lista = new List<EntidadePerguntas>();
                using var con = new SqlConnection(_connection);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                using(cmd = new SqlCommand(strSelect, con))
                {
                    cmd.Parameters.AddWithValue("@QuestionarioId", QuestionarioId);
                    using(Dr = await cmd.ExecuteReaderAsync())
                    {
                        EntidadePerguntas mod = null;
                        while (await Dr.ReadAsync())
                        {
                            mod = new EntidadePerguntas();
                            mod.QuestionarioId = Convert.ToInt32(Dr[0]);
                            mod.PerguntaId = Convert.ToInt32(Dr[1]);
                            mod.Pergunta = Convert.ToString(Dr[2]);
                            mod.QuestionarioTitulo = Convert.ToString(Dr[3]);
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

        public void Registrar(EntidadeResposta entidateResposta)
        {
            try
            {
                string strInsert = string.Format(@"INSERT INTO Resposta VALUES(@RespostaNome, @Perguntaid)");

                using var con = new SqlConnection(_connection);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                using(cmd = new SqlCommand(strInsert, con))
                {


                   cmd.Parameters.AddWithValue("@RespostaNome", entidateResposta.RespostaNome);
                   cmd.Parameters.AddWithValue("@PerguntaId", entidateResposta.PerguntaId);
                   cmd.ExecuteNonQuery();
                }
               

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
