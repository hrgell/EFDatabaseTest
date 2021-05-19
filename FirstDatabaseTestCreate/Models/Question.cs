using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace FirstDatabaseTestCreate.Models
{
    public class Question
    {
        // Opened questionnaire and how much to complete questionnaire can be stored in the fictive question 0.
        public int QuestionId { get; set; }
        [Required]
        public int QuestionnaireId { get; set; }
        [Required]
        public int Disabled { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
        [Required]
        public int QType { get; set; } // none, single choice, multi choice, text, scale
        [Required]
        public int Forced { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Notes { get; set; }
        [Required]
        public string Visible { get; set; }
        [Required]
        public int Weight { get; set; } // How much to complete question ?
        [Required]
        public string DefaultValue { get; set; }
        // For single, multi or scale questions that need an extra other option.
        [Required]
        public string TitleOther { get; set; }
        // For scale questions: the maximum vlaue, the title for the max and the title for the min.
        [Required]
        public int ScaleLimit { get; set; }
        [Required]
        public string TitleMax { get; set; }
        [Required]
        public string TitleMin { get; set; }
        [Required]
        public int InputOrder { get; set; } // 0 = display input after the answer title, 1 = display input before answer title
        [Required]
        public int Compact { get; set; } // 0 = question and answers on different lines, 1 = question and answers on two lines, 2 = question and answers on the same line
        [Required]
        public string QuestionTitleWidth { get; set; } // CSS value like "400px" or "60%"
        [Required]
        public string AnswerTitleWidth { get; set; } // CSS value
        public virtual Questionnaire Questionnaire { get; set; }
        [JsonIgnore, IgnoreDataMember] // Dont create embeded json
        public virtual List<Answer> Answers { get; set; }
    }
} // namespace
