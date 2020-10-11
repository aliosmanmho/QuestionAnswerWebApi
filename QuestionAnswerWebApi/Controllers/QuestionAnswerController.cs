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
        public ActionResult GetAnswer([FromQuery]string query)
        {
             if(string.IsNullOrEmpty(query))
                return  BadRequest("Query should not be empty");
           
           return Ok(questions.Where(x=>x.Query.ToUpper().Contains(query.ToUpper())));
        }
        [HttpPost]
        [Route("AddQuery")]
        public ActionResult AddQuery([FromBody]string query,string answer)
        {
            if(string.IsNullOrEmpty(query) ||string.IsNullOrEmpty(answer))
                return  BadRequest("Query and answer should not be empty");
            if(questions.Length>1000)
                return  BadRequest("There is a lot of data! Please delete data first.");
            if(questions.Any(x=>x.Query.ToUpper().Contains(query)))
                return  BadRequest("Question already attached");
           questions.Append(new Question(query:query,answer:answer));
           return Ok(questions);
        }
        [HttpPost]
        [Route("RemoveQuery")]
        public ActionResult RemoveQuery([FromQuery]string query)
        {
            if(string.IsNullOrEmpty(query) )
                return  BadRequest("Query should not be empty");
           questions =  questions.Where(x=>x.Query != query).ToArray();
           return Ok(questions);
        }
    }
}
