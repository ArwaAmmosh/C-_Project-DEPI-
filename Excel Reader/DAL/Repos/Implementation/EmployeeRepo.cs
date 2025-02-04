using DAL.DataBase;
using DAL.Entities;
using DAL.Repos.Abstraction;

namespace DAL.Repos.Implementation
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DBContext dbContext= new();
        public async void AddEmployee(Employee employee)
        {
           await dbContext.Employees.AddAsync(employee);
           dbContext.SaveChanges();
        }
    }
}
