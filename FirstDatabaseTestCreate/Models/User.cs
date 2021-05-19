using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace FirstDatabaseTestCreate.Models
{
    // Table
    //[Table(Name="Users")]
    public class User
    {
        //[Column(IsPrimaryKey = true)]
        public int UserId { get; set; }
        [Required]
        public int Disabled { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public int WantInfo { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string SecondaryEmail { get; set; }
        [Required]
        public string Pwd { get; set; } // TODO Find out how to use federated authentication
        [Required] 
        public string Phone { get; set; }
        [Required] 
        public string SecondaryPhone { get; set; }
        [Required]
        public string Homepage { get; set; }
        [Required]
        public string Notes { get; set; }
        [Required]
        public string Adr1 { get; set; }
        [Required] 
        public string Adr2 { get; set; }
        [Required] 
        public string Adr3 { get; set; }

        public int PostalCodeId { get; set; }
        public virtual PostalCode PostalCode { get; set; }

        [Required] 
        public string Adr4 { get; set; }

        [JsonIgnore, IgnoreDataMember] // Dont create embeded json
        public virtual List<Blog> Blogs { get; set; }
        [JsonIgnore, IgnoreDataMember] // Dont create embeded json
        public virtual List<Survey> Surveys { get; set; }
        [JsonIgnore, IgnoreDataMember] // Dont create embeded json
        public virtual List<Reply> Replies { get; set; }
    } // class
} // namespace
