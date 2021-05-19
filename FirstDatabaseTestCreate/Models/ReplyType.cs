using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FirstDatabaseTestCreate.Models
{
    // Table
    public class ReplyType
    {
        public int ReplyTypeId { get; set; }
        [Required]
        public int RType { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
        [Required]
        public string Title { get; set; }
    }
} // namespace
