using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class MomTodo
    {
        public MomTodo()
        {
            this.minutes = new Mom();
            this.todoList = new Todo();
        }
        public Mom minutes { get; set; }
        public Todo todoList { get; set; }
    }
}