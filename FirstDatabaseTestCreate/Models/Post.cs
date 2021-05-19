using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FirstDatabaseTestCreate.Models
{
    // Table
    public class Post
    {
        public int PostId { get; set; }
        [Required]
        public int Disabled { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    } // class
} // namespace
