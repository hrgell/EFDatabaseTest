using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace FirstDatabaseTestCreate.Models
{
    // Table
    public class Answer
    {
        public int AnswerId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public int Disabled { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public string Visible { get; set; }
        [Required]
        public string Title { get; set; }
        [JsonIgnore, IgnoreDataMember]
        public virtual Question Question { get; set; }
        //[JsonIgnore, IgnoreDataMember]
        [JsonIgnore, IgnoreDataMember] // Dont create embeded json
        public virtual List<Reply> Replies { get; set; }

    } // class
} // namespace
