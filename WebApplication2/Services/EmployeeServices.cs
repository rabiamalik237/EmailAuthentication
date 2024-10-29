using WebApplication2.Model;
namespace WebApplication2.Services
{
    public class EmployeeServices : IEmployee
    {
        private readonly List<Employee> _employeeList;
        public EmployeeServices()
        {
            _employeeList = new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    Name="test",
                    Job="abc",
                    Age=5,
                    Salary=20
                }

            };
        }
        public List<Employee> GetAllList()
        {
            return _employeeList;
        }
        public Employee GetById(int id)
        {
            return _employeeList.FirstOrDefault(dept => dept.Id == id);
        }
        public Employee AddEmployee(AddAndUpdate addAndUpdate)
        {
            var addEmployee = new Employee()
            {
                Id = _employeeList.Max(id => id.Id) + 1,
                Name = addAndUpdate.Name,
                Job = addAndUpdate.Job,
                Age = addAndUpdate.Age
            };
            _employeeList.Add(addEmployee);
            return addEmployee;
        }
        public Employee updateEmployee(int id,AddAndUpdate updateemployee)
        {
            var EmplIndex = _employeeList.FindIndex(idx => idx.Id == id);
            if (EmplIndex > 0)
            {
                var emp = _employeeList[EmplIndex];
               emp.Name = updateemployee.Name;
                emp.Salary = updateemployee.Salary;
                emp.Age = updateemployee.Age;
                emp.Job = updateemployee.Job;

                _employeeList[EmplIndex] = emp;

                return emp;

            }
            else
            {
                return null;
            }

        }
        public bool deleteById(int id)
        {
            var empindex = _employeeList.FindIndex(idx => idx.Id == id);
            if (empindex > 0)
            {
                _employeeList.RemoveAt(empindex);
            }
            return empindex > 0;
        }
    }
}
