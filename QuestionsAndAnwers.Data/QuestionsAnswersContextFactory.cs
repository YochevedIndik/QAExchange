using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionsAndAnwers.Data
{
    class QuestionsAnswersContextFactory: IDesignTimeDbContextFactory<QuestionsAnswersContext>
    {
        public QuestionsAnswersContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}QuestionsAndAnswers.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new QuestionsAnswersContext(config.GetConnectionString("ConStr"));
        }
    
    }
}
