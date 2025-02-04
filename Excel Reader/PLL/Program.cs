using BLL.Service;
using DAL.Entities;
using DAL.Repos.Abstraction;
using DAL.Repos.Implementation;
using System.Transactions;

namespace PLL
{
    public class Program
    {
        static void Main()
        {
            IEmployeeRepo repo = new EmployeeRepo();
            var employees = ExcelMapper.MapExcelToObjects<Employee>("C:\\Users\\EL-MASREYIA\\OneDrive\\Desktop\\Book1.xlsx");
            foreach (var employee in employees)
            {
                repo.AddEmployee(employee);
                Console.WriteLine(employee.Name);
            }
        }
    }
}
