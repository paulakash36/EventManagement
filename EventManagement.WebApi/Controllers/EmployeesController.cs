using EventManagement.Dal;
using EventManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ICommonRepository<Employee> _repository;
        public EmployeesController(ICommonRepository<Employee> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            var employees = _repository.GetAll();
            if (employees.Count == 0)
                return NotFound();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Employee>> GetDetails(int id)
        {
            var employee = _repository.GetDetails(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult Create(Employee employee)
        {
            _repository.Insert(employee);
            var result = _repository.SaveChanges();
            if (result > 0)
                return CreatedAtAction("GetDetails", new { id = employee.EmployeeId }, employee);
            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult Update(Employee employee)
        {
            _repository.Update(employee);
            var result = _repository.SaveChanges();
            if (result > 0)
                return NoContent();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<Employee> Delete(int id)
        {
            var employee = _repository.GetDetails(id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                _repository.Delete(employee);
                _repository.SaveChanges();
                return NoContent();
            }
        }


    }
}
