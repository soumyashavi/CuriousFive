using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class Mom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int momId { get; set; }
        [Required]
        [Display(Name ="MOM Title")]
        public string momTitle { get; set; }
        [Required]
        [Display(Name ="MOM Description")]
        public string momDescription { get; set; }
        [Required]
        [Display(Name ="Created On")]
        public DateTime createdOn { get; set; }
        [Required]
        [Display(Name ="Created By")]
        public string createdBy { get; set; }
        public virtual ICollection<Todo> Todo{ get; set; }
        

    }
}