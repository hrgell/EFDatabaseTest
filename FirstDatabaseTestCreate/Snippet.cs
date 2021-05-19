using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using FirstDatabaseTestCreate.Models;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FirstDatabaseTestCreate
{
    partial class Snippet
    {
        private static List<QryReplyOverview> GetUsersTheOldWay(MyContext db, string query)
        {
            List<QryReplyOverview> rst = new List<QryReplyOverview>();
            using (var command = db.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                db.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var elem = new QryReplyOverview
                            {
                                SurveyId = reader.GetInt32(0),
                                QuestionnaireId = reader.GetInt32(1),
                                QuestionTitle = reader.GetString(2),
                                QType = reader.GetInt32(3),
                                AnswerTitle = reader.GetString(4),
                                AnswerId = reader.GetInt32(5),
                                ReplyValue = reader.GetString(6),
                                RUserId = reader.GetInt32(7),
                            };
                            rst.Add(elem);
                        }
                    }
                }
            }
            return rst;
        } // method

        private static void Test1(MyContext db, Newtonsoft.Json.Formatting fmt, int userId, int SurveyId)
        {
            int RUserId = 0;
            string query =
                "select s.SurveyId, qn.QuestionnaireId, q.Title as QuestionTitle, q.QType, ISNULL(a.Title, '') as AnswerTitle, ISNULL(a.AnswerId, 0) as AnswerId, ISNULL(r.Value, '') AS ReplyValue, ISNULL(r.UserId, 0) AS RUserId " + Environment.NewLine +
                "from users u " + Environment.NewLine +
                "left join Surveys s on s.UserId = u.UserId " + Environment.NewLine +
                "left join Questionnaires qn on qn.QuestionnaireId = s.Questionnaireid " + Environment.NewLine +
                "left join Questions q on q.QuestionnaireId = qn.QuestionnaireId " + Environment.NewLine +
                "left join Answers a on a.QuestionId = q.QuestionId " + Environment.NewLine +
                "left join Replies r on r.AnswerId = a.AnswerId " + Environment.NewLine;
            string sep = "where ";
            if (userId != 0)
            {
                query += sep + "u.UserId = " + userId + " ";
                sep = "and ";
            }
            if (SurveyId != 0)
            {
                query += sep + "s.SurveyId = " + SurveyId + " ";
                sep = "and ";
            }
            if (RUserId != 0)
            {
                query += sep + "r.RUserId = " + RUserId + " ";
                sep = "and ";
            }
            if (sep != "where ")
                query += Environment.NewLine;
            query += "order by u.Email, qn.Title, q.DisplayOrder, a.DisplayOrder, r.UserId";

            // works
            //var txt = JsonConvert.SerializeObject(GetUsersTheOldWay(db, query), fmt);
            //Util.WriteLine(txt);

            var ureplies = db.ReplyOverview.FromSqlRaw<QryReplyOverview>(query).AsNoTracking();
            var txt = JsonConvert.SerializeObject(ureplies.ToList<QryReplyOverview>(), fmt);
            Util.WriteLine(txt);

            Util.WriteLine(query);
        }

        private static void Test2(MyContext db, Newtonsoft.Json.Formatting fmt, int userId, int SurveyId)
        {
            var rst = from s in db.Surveys.Where(e => e.SurveyId == SurveyId)
                      join qn in db.Questionnaires on s.QuestionnaireId equals qn.QuestionnaireId
                      join q in db.Questions on qn.QuestionnaireId equals q.QuestionnaireId
                      join a in db.Answers on q.QuestionId equals a.QuestionId into a3
                      from a in a3.DefaultIfEmpty()
                      join r2 in db.Replies.Where(e => (e.UserId == userId) && (e.SurveyId == SurveyId)) on a.AnswerId equals r2.AnswerId into r3
                      from r in r3.DefaultIfEmpty()
                      orderby q.DisplayOrder, a.DisplayOrder
                      select new { s.SurveyId, qn.QuestionnaireId, QuestionTitle = q.Title, q.QType, AnswerTitle = a.Title, a.AnswerId, r.RType, r.Value, r.UserId };
            var txt = JsonConvert.SerializeObject(rst.ToList(), fmt);
            Util.WriteLine("---");
            Util.WriteLine(txt);
        }

        private static void Test3(MyContext db, Newtonsoft.Json.Formatting fmt, int userId, int SurveyId)
        {
            var rst = from s in db.Surveys.Where(e => e.SurveyId == SurveyId)
                      join qn in db.Questionnaires on s.QuestionnaireId equals qn.QuestionnaireId
                      join q in db.Questions on qn.QuestionnaireId equals q.QuestionnaireId
                      join a2 in db.Answers on q.QuestionId equals a2.QuestionId into a3
                      from a in a3.DefaultIfEmpty()
                      join r2 in db.Replies.Where(e => (e.UserId == userId) && (e.SurveyId == SurveyId)) on a.AnswerId equals r2.AnswerId into r3
                      from r in r3.DefaultIfEmpty()
                      orderby q.DisplayOrder, a.DisplayOrder
                      //group new { s.SurveyId, qn.QuestionnaireId, QuestionTitle = q.Title, q.QType, Vals = new { AnswerTitle = a.Title, a.AnswerId, r.RType, r.Value, r.UserId } } by a.QuestionId into NewGroup
                      //select NewGroup;
                      select new { s.SurveyId, qn.QuestionnaireId, QuestionTitle = q.Title, q.QType, AnswerTitle = a.Title, a.AnswerId, r.RType, r.Value, r.UserId };
            var rst2 = rst.AsEnumerable().GroupBy(e => e.QuestionTitle).ToDictionary(x => x.Key, y => y);
            var txt = JsonConvert.SerializeObject(rst2, fmt);
            Util.WriteLine("---");
            Util.WriteLine(txt);
        }

        private static void Test4(MyContext db, Newtonsoft.Json.Formatting fmt, int userId, int SurveyId)
        {
            //var rst = from s in db.Surveys.Where(e => e.SurveyId == SurveyId)
            //          join qn in db.Questionnaires on s.QuestionnaireId equals qn.QuestionnaireId
            //          join q in db.Questions on qn.QuestionnaireId equals q.QuestionnaireId
            //          join a2 in db.Answers on q.QuestionId equals a2.QuestionId into a3
            //          from a in a3.DefaultIfEmpty()
            //          join r2 in db.Replies.Where(e => (e.UserId == userId) && (e.SurveyId == SurveyId)) on a.AnswerId equals r2.AnswerId into r3
            //          from r in r3.DefaultIfEmpty()
            //          orderby q.DisplayOrder, a.DisplayOrder
            //          select new { QuestionTitle = q.Title, q.QType, AnswerTitle = a.Title, a.AnswerId, r.RType, r.Value, r.UserId };
            // todo       
            //var rst2 = rst.AsEnumerable().GroupBy(e => e.QuestionTitle).ToDictionary(x => x.Key, x => x.Select(new { QuestionTitle = x.Title, x.QType, AnswerTitle = a.Title, x.AnswerId, r.RType, r.Value, r.UserId }));
            //var txt = JsonConvert.SerializeObject(rst2, fmt);
            //Util.WriteLine("---");
            //Util.WriteLine(txt);
        }

        static void MainTest(string[] args)
        {
            Util.WriteLine("---------- start");

            //// Testing config file location problems. Had to use static connection string as fall back.
            //ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            //map.ExeConfigFilename = "FirstDatabaseTestCreate.dll.config";
            //Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            //Util.WriteLine("Constructed: " + config.ConnectionStrings.ConnectionStrings["DefaultConnection"].ToString());

            var options = new DbContextOptionsBuilder<MyContext>();
            using (var db = new MyContext(options.Options))
            {
                var script = db.Database.GenerateCreateScript();
                Util.WriteLine(script);

                //db.Database.EnsureDeleted();
                var created = db.Database.EnsureCreated();
                Util.WriteLine("db.Database.EnsureCreated(): " + created.ToString());
                Test1(db, Newtonsoft.Json.Formatting.Indented, 1, 1);
            }
            Util.WriteLine("---------- stop");
        } // MainTest()

    } // class
} // namespace


