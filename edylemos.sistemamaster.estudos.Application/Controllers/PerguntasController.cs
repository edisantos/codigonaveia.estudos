using edylemos.sistemamaster.estudos.Domain.Entidades.Questionarios;
using edylemos.sistemamaster.estudos.Services.Interface.Questionario;
using Microsoft.AspNetCore.Mvc;

namespace edylemos.sistemamaster.estudos.Application.Controllers
{
    public class PerguntasController : Controller
    {
        private readonly IPerguntaServices _perguntaServices;

        public PerguntasController(IPerguntaServices perguntaServices)
        {
            _perguntaServices = perguntaServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RegistrarPerguntas(int QuestionarioId)
        {

           var result = await  _perguntaServices.ObterPerguntasPorId(QuestionarioId);

            if (result != null)
            {
                ViewBag.ListaPergunta = result;
               
            }
            
            return View(new EntidadePerguntas { QuestionarioId = QuestionarioId});
        }
        [HttpPost]
        public IActionResult RegistrarPerguntas(EntidadePerguntas entidadePergunta, int QuestionarioId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entidadeQuestionario = new EntidadeQuestionario();

                    entidadePergunta.QuestionarioId = QuestionarioId;
                    _perguntaServices.Registrar(entidadePergunta);
                    TempData["MensagemSucesso"] = "Pergunta criada com sucesso! Adicione mais ...";
                    return RedirectToAction(nameof(RegistrarPerguntas), new { QuestionarioId });
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return View();
        }
    }
}
