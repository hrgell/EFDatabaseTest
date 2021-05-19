using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using FirstDatabaseTestCreate.Models;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using Fmt = Newtonsoft.Json.Formatting;

namespace FirstDatabaseTestCreate
{
    partial class Program
    {
        public static int MainJob(MyContext db, int UserId, int SurveyId)
        {
            var fmt = Fmt.Indented;
            var pretty = (fmt == Fmt.Indented);

             var users = db.Users.Where(e => e.UserId == UserId);
            if (!users.Any())
                return Util.WriteLine("MainJob: No user found.");
            var user = users.First();

            var surveys = db.Surveys.Where(e => e.SurveyId == SurveyId);
            if (!surveys.Any())
                return Util.WriteLine("MainJob: No survey found.");
            var survey = surveys.First();
            int QuestionnaireId = survey.QuestionnaireId;

            var questionnaires = db.Questionnaires.Where(e => e.QuestionnaireId == QuestionnaireId);
            if (!questionnaires.Any())
                return Util.WriteLine("MainJob: No questionnaire found.");
            var questionnaire = questionnaires.First();

            var questions = db.Questions.Where(e => e.QuestionnaireId == QuestionnaireId).OrderBy(e => e.DisplayOrder);
            if (!questions.Any())
                return Util.WriteLine("MainJob: No questions found.");

            var answers = from a in db.Answers
                          join q in db.Questions on a.QuestionId equals q.QuestionId
                          where q.QuestionnaireId == QuestionnaireId
                          orderby q.DisplayOrder, a.DisplayOrder, a.Title
                          select a;
            if (!answers.Any())
                return Util.WriteLine("MainJob: No answers found.");

            var replies = from a in db.Answers
                          join r in db.Replies on a.AnswerId equals r.AnswerId
                          where r.SurveyId == SurveyId && r.UserId == UserId
                          select r; // new QryReply { AnswerId = r.AnswerId, RType = r.RType, Value = r.Value };
            if (!replies.Any())
                return Util.WriteLine("MainJob: No replies found.");

            var atxt = JsonConvert.SerializeObject(answers.ToList(), fmt);
            var utxt = JsonConvert.SerializeObject(user, fmt);
            var stxt = JsonConvert.SerializeObject(survey, fmt);
            var qntxt = JsonConvert.SerializeObject(questionnaire, fmt);
            var qtxt = JsonConvert.SerializeObject(questions, fmt);
            var rtxt = JsonConvert.SerializeObject(replies.ToList(), fmt);

            var nl = Environment.NewLine;
            StringBuilder buf = new StringBuilder();
            buf.Append("[{");
            if (pretty) buf.Append(nl);
            buf.Append("\"user\":").Append(utxt);
            buf.Append(",\"survey\":").Append(stxt);
            buf.Append(",\"questionnaire\":").Append(qntxt);
            buf.Append(",\"questions\":").Append(qtxt);
            buf.Append(",\"answers\":").Append(atxt);
            buf.Append(",\"replies\":").Append(rtxt);
            if (pretty) buf.Append(nl);
            buf.Append("}]");
            Util.WriteLine(buf.ToString());

            //Util.WriteLine("---------------------------------------");
          //Util.WriteLine("[{ survey = " + JsonConvert.SerializeObject(survey, fmt) + "}]");
            return 1;
        } // MainJob()

        static void Main(string[] args)
        {
            //Util.WriteLine("---------- start");
            var options = new DbContextOptionsBuilder<MyContext>();
            using (var db = new MyContext(options.Options))
            {
                db.Database.EnsureDeleted();
                var created = db.Database.EnsureCreated();
                //Util.WriteLine("db.Database.EnsureCreated(): " + created.ToString());
                MainJob(db, 1, 1);
            }
            //Util.WriteLine("---------- stop");
        } // Main()
    } // class
} // namespace
