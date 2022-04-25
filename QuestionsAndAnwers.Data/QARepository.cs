using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestionsAndAnwers.Data
{
    public class QARepository
    {
        private readonly string _connectionString;
        public QARepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private Tag GetTag(string name)
        {
            using var context = new QuestionsAnswersContext(_connectionString);
            return context.Tags.FirstOrDefault(t => t.Name == name);
        }

        private int AddTag(string name)
        {
            using var context = new QuestionsAnswersContext(_connectionString);
            var tag = new Tag { Name = name };
            context.Tags.Add(tag);
            context.SaveChanges();
            return tag.Id;
        }
        public void AddQuestion(Question question, IEnumerable<string> tags)
        {
            using var context = new QuestionsAnswersContext(_connectionString);
            context.Questions.Add(question);
            context.SaveChanges();
            foreach (string tag in tags)
            {
                Tag t = GetTag(tag);
                int tagId;
                if (t == null)
                {
                    tagId = AddTag(tag);
                }
                else
                {
                    tagId = t.Id;
                }
                context.QuestionsTags.Add(new QuestionsTagsJoin
                {
                    QuestionId = question.Id,
                    TagId = tagId
                });
            }

            context.SaveChanges();

        }
        public void AddUser(User user)
        {
            using var context = new QuestionsAnswersContext(_connectionString);
            context.Users.Add(user);
            context.SaveChanges();
        }
        public void AddAnswer(Answer answer)
        {
            using var context = new QuestionsAnswersContext(_connectionString);
            context.Answers.Add(answer);
            context.SaveChanges();
        }
        public List<Question> GetQuestions()
        {
            using var context = new QuestionsAnswersContext(_connectionString);
         return context.Questions.Include(q => q.User).Include(q => q.Likes)
                .Include(q => q.Answers).Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag).ToList();
        }
        public Question GetQuestionById(int id)
        {
            using var context = new QuestionsAnswersContext(_connectionString);
            return context.Questions.Include(q => q.User).Include(q => q.Likes).Include(q => q.Answers)
                .Include(q => q.QuestionsTags).ThenInclude(qt => qt.Tag).ToList().FirstOrDefault(q => q.Id == id);
        }
        public void Signup(User user, string password)
        {
            using var context = new QuestionsAnswersContext(_connectionString);
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            context.Users.Add(user);
            context.SaveChanges();
        }
        public User Login(string email, string password)
        {
            using var context = new QuestionsAnswersContext(_connectionString);
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return isValid ? user : null;

        }
        public User GetUserByEmail(string email)
        {
            using var context = new QuestionsAnswersContext(_connectionString);
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
    }

}
