using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FirstDatabaseTestCreate.Models
{
    public class QryReplyOverview
    {
        public int SurveyId { get; set; }
        public int QuestionnaireId { get; set; }
        public string QuestionTitle { get; set; }
        public int QType { get; set; }
        public string AnswerTitle { get; set; }
        public int AnswerId { get; set; }
        public string ReplyValue { get; set; }
        public int RUserId { get; set; }
    }
}
