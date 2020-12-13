using System.Data.Entity;

namespace Homework_18.Model
{
    class BankDBContext : DbContext
    {
        public BankDBContext()
           : base("DBConnectionServer")
        { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
    }
}
