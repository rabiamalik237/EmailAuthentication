using WebApplication2.Model;

namespace WebApplication2.Services
{
    public interface IEmployee
    {
        List<Employee> GetAllList();
        Employee GetById(int id);
        Employee AddEmployee(AddAndUpdate addAndUpdate);
        Employee updateEmployee(int id, AddAndUpdate addAndUpdate);
        bool deleteById(int id);

    }
}
