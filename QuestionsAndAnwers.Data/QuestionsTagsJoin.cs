using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionsAndAnwers.Data
{
   public class QuestionsTagsJoin
    {
        public int QuestionId { get; set; }
        public int TagId { get; set; }

        public Question Question { get; set; }
        public Tag Tag { get; set; }

    }
}
