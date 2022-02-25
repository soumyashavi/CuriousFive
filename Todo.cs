using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int todoId { get; set; }
        [Required]
        public int momId { get; set; }
        [Required]
        [Display(Name ="Task Description")]
        public string taskDescription { get; set; }
        [Required]
        [Display(Name ="Employee Id")]
        public string empId { get; set; }
        [Required]
        [Display(Name ="Due Date")]
        public DateTime dueDate { get; set; }
        [Required]
        [Display(Name ="Is Active")]
        public bool isActive { get; set; }

        public virtual User user { get; set; }
        public virtual Mom mom { get; set; }

    }
}