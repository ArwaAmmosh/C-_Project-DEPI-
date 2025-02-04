using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataBase
{
    public class DBContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-A0LMSG6\\SD;Database=ExcelDataBase;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true ");
        }
        public DbSet<Employee> Employees { get; set; }
    }
}