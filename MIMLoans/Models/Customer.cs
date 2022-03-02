using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MIMLoans.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 2)]
        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime DateOfBirthUtc { get; set; }

        [Required]
        public string EmailAddress { get; set; }
        public bool HomeOwner { get; set; }
        public DateTime DateAddedUtc { get; set; }

    }
}
