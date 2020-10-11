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
       private static readonly Question[] questions = new []{
           new Question(){
               Query=@"Query Example 1",
                Answer = "Answer 1"
           },
            new Question(){
                        Query=@"Query Example 2",
                            Answer = "Answer 2"
                    },
            new Question(){
                                    Query=@"Query Example 3",
                            Answer = "Answer 3"
                    }, 
            new Question(){
                                    Query=@"Query Example 4",
                            Answer = "Answer 4"
                    },
                    
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
        public IEnumerable<Question> GetAnswer([FromQuery]string query)
        {
           return questions.Where(x=>x.Query.Contains(query));
        }
    }
}
