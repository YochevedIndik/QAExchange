using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QuestionsAndAnswers.Web.Models;
using QuestionsAndAnwers.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsAndAnswers.Web.Controllers
{
    public class HomeController : Controller
    {

        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new QARepository(_connectionString);
           List<Question> questions = repo.GetQuestions();
            QuestionsViewModel vm = new QuestionsViewModel { Questions = questions };
            return View(vm);
        }
        [Authorize]
        public IActionResult AddQuestion()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddQuestion(Question question, List<string> tags)
        {
            question.Date = DateTime.Now;
            var repo = new QARepository(_connectionString);
            repo.AddQuestion(question, tags);
            return Redirect("/");
        }
        public IActionResult ViewQuestion(int id)
        {
            var repo = new QARepository(_connectionString);
            Question question = repo.GetQuestionById(id);
            var vm = new ViewQuestionVm { Question = question };
            return View(vm);
        }
        [HttpPost]
        public IActionResult AddAnswer(Answer answer)
        {
            answer.Date = DateTime.Now;
            var repo = new QARepository(_connectionString);
            repo.AddAnswer(answer);
            return Redirect($"/home/ViewQuestion?id={answer.QuestionId}");
        }


    }
}
