using edylemos.sistemamaster.estudos.Domain.Entidades.Questionarios;
using edylemos.sistemamaster.estudos.Services.Interface.Questionario;
using Microsoft.AspNetCore.Mvc;

namespace edylemos.sistemamaster.estudos.Application.Controllers
{
    public class QuestionarioController : Controller
    {
        private readonly IQuestionarioServices _questionarioServices;

        public QuestionarioController(IQuestionarioServices  questionarioServices)
        {
            _questionarioServices = questionarioServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registrar(EntidadeQuestionario entidadeQuestionario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _questionarioServices.Registrar(entidadeQuestionario);
                    TempData["SucessoMensagem"]= $"Questionario {entidadeQuestionario.QuestionarioTitulo} Criado com sucesso!";
                    return RedirectToAction("RegistrarPerguntas","Perguntas", new {entidadeQuestionario.QuestionarioId});
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            return View(entidadeQuestionario);
        }

        public async Task<IActionResult> ListaQuestionario()
        {

            var result = await _questionarioServices.ObterQuestionario();
            return View(result.ToList());
        }
    }
}
