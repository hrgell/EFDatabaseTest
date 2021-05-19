using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
namespace FirstDatabaseTestCreate.Models
{
    public class Survey
    {
        public int SurveyId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int QuestionnaireId { get; set; }
        [Required]
        public DateTime DateBegin { get; set; }
        [Required]
        public DateTime DateEnd { get; set; }
        [Required]
        public int Disabled { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int UseTitle { get; set; } // If 0 use the questionnaire title, otherwise use the survey title
        [Required]
        public int Visible { get; set; }
        [JsonIgnore, IgnoreDataMember]
        public virtual User User { get; set; }
        [JsonIgnore, IgnoreDataMember] // Dont create embeded json
        public virtual Questionnaire Questionnaire { get; set; }
    }
} // namespace
