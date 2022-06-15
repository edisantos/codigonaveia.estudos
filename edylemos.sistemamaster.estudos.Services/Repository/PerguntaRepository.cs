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
    public class PerguntaRepository : IPerguntaServices
    {
        readonly string _connection;
        private SqlCommand? cmd;
        private SqlDataReader? Dr;
        public PerguntaRepository(IConfiguration config)
        {
            _connection = config.GetConnectionString("DefaultConnection");
        }
        public Task<IEnumerable<EntidadePerguntas>> ObterPerguntas()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PerguntaId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<EntidadePerguntas>> ObterPerguntasPorId(int PerguntaId)
        {
            try
            {
                string strInsert = string.Format(@"SELECT PerguntaId, Pergunta FROM Perguntas WHERE QuestionarioId = @PerguntaId");
                List<EntidadePerguntas> lista = new List<EntidadePerguntas>();
                using var con = new SqlConnection(_connection);

                if (con.State == ConnectionState.Closed)
                    con.Open();
                using (cmd = new SqlCommand(strInsert, con))
                {
                    cmd.Parameters.AddWithValue("@PerguntaId", PerguntaId);
                    using (Dr = await cmd.ExecuteReaderAsync())
                    {
                        EntidadePerguntas mod = null;

                        while (await Dr.ReadAsync())
                        {
                            mod = new EntidadePerguntas();
                            mod.PerguntaId = Convert.ToInt32(Dr[0]);
                            mod.Pergunta = Convert.ToString(Dr[1]);
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

        public void Registrar(EntidadePerguntas entidadePergunta)
        {
            try
            {
                string strInsert = string.Format(@"INSERT INTO Perguntas VALUES(SYSDATETIME(),@Pergunta,@QuestionarioId,NULL)");

                using var con = new SqlConnection(_connection);

                if (con.State == ConnectionState.Closed)
                    con.Open();
                using (cmd = new SqlCommand(strInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Pergunta", entidadePergunta.Pergunta);
                    cmd.Parameters.AddWithValue("@QuestionarioId", entidadePergunta.QuestionarioId);
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
