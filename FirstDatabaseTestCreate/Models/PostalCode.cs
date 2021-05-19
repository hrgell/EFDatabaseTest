using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FirstDatabaseTestCreate.Models
{
    public class PostalCode
    {
        public int PostalCodeId { get; set; }
        [Required]
        public int Disabled { get; set; }
        [Required]
        public string CountryCode { get; set; } // TODO: Use country table.
        [Required]
        public string Province { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string City { get; set; }
    } // class
} // namespace
