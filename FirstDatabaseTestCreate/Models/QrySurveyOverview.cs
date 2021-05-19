using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FirstDatabaseTestCreate.Models
{
    public class QrySurveyOverview
    {
        public int SurveyId { get; set; }
        public int QuestionnaireId { get; set; }
        public string Title { get; set; }
        public string QuestionTitle { get; set; }
        public int UseTitle { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
    } // class
} // namespace
