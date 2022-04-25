using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionsAndAnwers.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public List<Answer> Answers { get; set; }
        public List<QuestionsTagsJoin> QuestionsTags { get; set; }
        public List<Likes> Likes { get; set; }
    }
}
