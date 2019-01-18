using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerWorld.Models
{
    public class Contact
    {
        public int ID { get; set; }
        [StringLength(100)]
        [Required]
        public string FullName { get; set; }
        [StringLength(100)]
        [Required]
        public string Email { get; set; }
        [StringLength(1000)]
        [Required]
        public string Message { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; }
    }
}
