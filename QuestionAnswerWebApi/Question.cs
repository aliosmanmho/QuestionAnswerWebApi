using System;

namespace QuestionAnswerApi
{
    public class Question
    {
        public Question(string query,string answer)
        {
            this.Query = query;
            this.Answer = answer;
        }
        public string Query { get; set; }

        public string Answer { get; set;}
    }
}
