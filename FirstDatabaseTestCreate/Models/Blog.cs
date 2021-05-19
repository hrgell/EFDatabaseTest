using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FirstDatabaseTestCreate.Models
{
    // Table
    public class Blog
    {
        public int BlogId { get; set; }
        public int Disabled { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Post> Posts { get; set; }
    } // class
} // namespace
