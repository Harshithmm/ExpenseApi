using Microsoft.EntityFrameworkCore;

namespace ExpensesApi.Model
{
    public class AppDbContext:DbContext
    {
        private AppDbContext _context;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Seed();
        }
        public DbSet<Entry> Entries { get; set; }

        public DbSet<User> Users { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Entry>().HasData(
        //        new  { IsExpense = true, Description = "Dummy1", Value = 10 },
        //        new  { IsExpense = false, Description = "Dummy2", Value = 100 }
        //        // Add more dummy data as needed
        //    );
        //        _context.SaveChanges();
        //}
    }
}
