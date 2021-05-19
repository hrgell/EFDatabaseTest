using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace FirstDatabaseTestCreate.Models
{
    public class Questionnaire
    {
        public int QuestionnaireId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Disabled { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Weight { get; set; } // Weight of all questions must be greater than or equal.
        [Required]
        public string Visible { get; set; }
        [JsonIgnore, IgnoreDataMember] // Dont create embeded json
        public virtual List<Question> Questions { get; set; }
        [JsonIgnore, IgnoreDataMember]
        public virtual User User { get; set; }
        [JsonIgnore, IgnoreDataMember]
        public virtual List<Survey> Surveys { get; set; }
    }
} // namespace
