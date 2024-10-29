using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;
        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet("Alldjfklfjdks")]
        [HttpGet("All")]
        public IActionResult Get()
        {
            return Ok(_employee.GetAllList());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var emp = _employee.GetById(id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        [HttpPost("save")]
        public IActionResult Post(AddAndUpdate obj)
        {
            var emp = _employee.AddEmployee(obj);
            if (emp == null)
            {
                return BadRequest();
            }
            return Ok(new
            {
                message = "Employee Created Successfully...!",
                id = emp!.Id
            });
        }
        [HttpPut("update")]
        public IActionResult Put(int id, AddAndUpdate obj)
        {
            var emp = _employee.updateEmployee(id, obj);
            if (emp == null)
            {
                return BadRequest();
            }
            return Ok(new
            {
                message = "Employee Updated Successfully...!",
                id = emp!.Id
            });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!(_employee.deleteById(id)))
            {
                return NotFound();
            }
            return Ok(new
            {
                message = "Employee deleted Successfully...!",
                Id = id
            });
        }
    }
}
