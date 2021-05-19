using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FirstDatabaseTestCreate.Models
{
    // Table
    public class QuestionType
    {
        public int QuestionTypeId { get; set; }
        [Required]
        public int QType { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
        [Required]
        public string Title { get; set; }
    }
} // namespace
