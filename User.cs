using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class User
    {
        [Key]
        [Required]
        [Display(Name ="Employee Id")]
        public string empId { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Email Id")]
        public string emailId { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string name { get; set; }
        [Required]
        [Display(Name ="Mobile No.")]
        public string mobileNo { get; set; }
        [Required]
        [Display(Name ="Password")]
        public string password { get; set; }
        [Required]
        [Display(Name ="Manager Id")]
        public string managerId { get; set; }
        [Required]
        public bool isTowerLead { get; set; }
    }
}