using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace QuestionAnswerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionAnswerController : ControllerBase
    {
       private static  Question[] questions = new []{
           new Question(
               query:@"Query Example 1",
                answer : "Answer 1"
           ),
            new Question(
                        query:@"Query Example 2",
                            answer : "Answer 2"
            ),
            new Question(
                                    query:@"Query Example 3",
                            answer : "Answer 3"
            ), 
            new Question(
                                    query:@"Query Example 4",
                            answer : "Answer 4"
            ),
                    
        };

        private readonly ILogger<QuestionAnswerController> _logger;

        public QuestionAnswerController(ILogger<QuestionAnswerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Question> Get()
        {
           return questions;
        }
        [HttpPost]
        [Route("GetAnswer")]
        public ActionResult GetAnswer([FromBody]Question question)
        {
             if(question ==null || string.IsNullOrEmpty(question.Query))
                return  BadRequest("Query should not be empty");
           
           return Ok(questions.Where(x=>x.Query.ToUpper().Contains(question.Query.ToUpper())));
        }
        [HttpPost]
        [Route("AddQuery")]
        public ActionResult AddQuery([FromBody]Question question)
        {
            if(question==null || string.IsNullOrEmpty(question.Query) ||string.IsNullOrEmpty(question.Answer))
                return  BadRequest("Query and answer should not be empty");
            if(questions.Length>1000)
                return  BadRequest("There is a lot of data! Please delete data first.");
            if(questions.Any(x=>x.Query.ToUpper().Contains(question.Query)))
                return  BadRequest("Question already attached");
           questions.Append(question);
           return Ok(questions);
        }
        [HttpPost]
        [Route("RemoveQuery")]
        public ActionResult RemoveQuery([FromBody]Question question)
        {
            if(question==null || string.IsNullOrEmpty(question.Query) )
                return  BadRequest("Query should not be empty");
           questions =  questions.Where(x=>x.Query != question.Query).ToArray();
           return Ok(questions);
        }
    }
}
