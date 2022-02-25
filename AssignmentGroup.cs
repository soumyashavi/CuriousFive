using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication9.Models
{
    public class AssignmentGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int agId { get; set; }
        [Required]
        [Display(Name ="AG Name")]
        public string agName { get; set; }
        [Required]
        public int teamId { get; set; }
        [Required]
        [Display(Name ="Lead Name")]
        public string leadName   { get; set; }

        public virtual ICollection<UserAssigned> UserAssigned { get; set; }
        public virtual Team team { get; set; }
    }
}