using System.Data.Entity;

namespace Homework_13.Model
{
    class HW13Context : DbContext
    {
        public HW13Context() : base("DefaultConnection")
        {
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
