using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace WebApplication9.Models
{
    public class Roster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int rosterId { get; set; }
        [Required]
        public int userAssignedId { get; set; }
        [Required]
        [Display(Name ="Single Day task")]
        public bool isOneDay { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Start Date")]
        public DateTime startDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="End Date")]
        public DateTime endDate { get; set; }
        [Required]
        [Display(Name ="OnCall Person")]
        public String onCall { get; set; }
        [Required]
        [Display(Name ="Shift")]
        public string shift { get; set; }

        public virtual UserAssigned userassigned { get; set; }
        
    }
    public enum onCall
    {
        
    primary =1,
        secondary=2,
        NotApplied=3
    }
    public enum shift
    {
        general,
        night
    }
}