using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication9.Models
{
    public class UserAssigned
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int userassignedId { get; set; }
        [Display(Name ="Employee Id")]
        [Required]
        public string empId { get; set; }
        [Required]
        public int agId { get; set; }
        public virtual ICollection<Roster> Rosters { get; set; }

        public virtual User user { get; set; }
        public virtual AssignmentGroup assignmentgroup { get; set; }
    }
}