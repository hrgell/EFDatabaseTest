using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace FirstDatabaseTestCreate.Models
{
    // Table
    public class Reply
    {
        public int ReplyId { get; set; }
        [Required]
        public int AnswerId { get; set; }
        [Required]
        public int Disabled { get; set; }
        [Required]
        public int SurveyId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RType { get; set; }
        [Required]
        public string Value { get; set; }
        [JsonIgnore, IgnoreDataMember]
        public virtual User User { get; set; }
        [JsonIgnore, IgnoreDataMember]
        public virtual Answer Answer { get; set; }
    } // class

}
