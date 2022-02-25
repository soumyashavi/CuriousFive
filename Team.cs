using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebApplication9.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int teamId { get; set; }
        [Required]
        [Display(Name ="Team Name")]
        public string teamName { get; set; }

        public virtual ICollection<AssignmentGroup> AssignmentGroups { get; set; }
    }
}