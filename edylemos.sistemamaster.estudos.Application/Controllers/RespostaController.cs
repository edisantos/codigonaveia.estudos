using edylemos.sistemamaster.estudos.Domain.Entidades.Questionarios;
using edylemos.sistemamaster.estudos.Services.Interface.Questionario;
using Microsoft.AspNetCore.Mvc;


namespace edylemos.sistemamaster.estudos.Application.Controllers
{
    public class RespostaController : Controller
    {
        private readonly IRespostaServices _respostaServices;

        public RespostaController(IRespostaServices respostaServices)
        {
            _respostaServices = respostaServices;
        }
        public async Task<IActionResult> RegistroResposta(int QuestionarioId)
        {
            var result = await _respostaServices.ObterRespostaPorQuestionarioId(QuestionarioId);
            ViewBag.Perguntas = result.ToList();
            ViewBag.QuestionarioTitulo = result.FirstOrDefault().QuestionarioTitulo;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegistroResposta(EntidadeResposta entidadeResposta, int QuestionarioId, string[] RespostaNome)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var registroGrupo = await _respostaServices.ObterRespostaPorQuestionarioId(QuestionarioId);
                    //int contador = 0;
                    //var array = RespostaNome.ToArray();
                    /*
                     * CRIADO POR EDINALDO SANTOS - 10/06/2022
                      PASSA NO FOREACH Cada Id da Resposta
                     
                     */
                    //foreach (var registro in registroGrupo)
                    //{
                    //    //For Verifica cada passada para adicionar a resposta para cada pergunta
                    //    contador++;
                    //    for (int i =0; i < contador; i++)
                    //    {
                    //       entidadeResposta.RespostaNome = array[i].ToString();
                    //    }
                    //    int perguntaId = registro.PerguntaId;
                    //    entidadeResposta.PerguntaId = perguntaId;

                    //    _respostaServices.Registrar(entidadeResposta);

                    //}
                    var registroGrupo = await _respostaServices.ObterRespostaPorQuestionarioId(QuestionarioId);
                    int contador = 0;
                    var array = RespostaNome.ToArray();
                    registroGrupo.ToList().ForEach(r =>
                    {
                        contador++;
                        for (int i = 0; i < contador; i++)
                        {
                            entidadeResposta.RespostaNome = array[i].ToString();
                        }

                        int perguntaId = r.PerguntaId;
                        entidadeResposta.PerguntaId = perguntaId;
                        _respostaServices.Registrar(entidadeResposta);
                    });

                    TempData["Success"] = "Resposta respondida com sucesso!";
                    return RedirectToAction(nameof(RespostaRespondida));
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return View();
        }

        public IActionResult RespostaRespondida()
        {
            return View();
        }
    }
}
