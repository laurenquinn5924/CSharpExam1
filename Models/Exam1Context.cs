using Microsoft.EntityFrameworkCore;

namespace CSharpExam1.Models
{
    public class Exam1Context : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public Exam1Context(DbContextOptions<Exam1Context> options) : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Activity> Activities { get; set; }

        public DbSet<RSVP> RSVPS { get; set; }


    }
}
