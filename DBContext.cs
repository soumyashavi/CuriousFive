using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace WebApplication9.Models
{
    public class DBContext:DbContext
    {
        public DBContext():base("Name=dbconfig")
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<UserAssigned> userAgs{ get; set; }
        public DbSet<Todo> todos { get; set; }
        public DbSet<Team> teams { get; set; }
        public DbSet<Roster> rosters { get; set; }
        public DbSet<Mom> moms { get; set; }
        public DbSet<AssignmentGroup> assignmentGroups { get; set; }



    }
}