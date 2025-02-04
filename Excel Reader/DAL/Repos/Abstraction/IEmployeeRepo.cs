
using DAL.Entities;

namespace DAL.Repos.Abstraction
{
    public interface IEmployeeRepo
    {
        public void AddEmployee(Employee employee);
    }
}
