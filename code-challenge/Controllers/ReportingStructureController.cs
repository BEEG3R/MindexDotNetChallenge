using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace challenge.Controllers
{
    [Route("api/reporting")]
    public class ReportingStructureController : Controller {
        private readonly IEmployeeService employeeService;

        public ReportingStructureController(IEmployeeService employeeService) {
            this.employeeService = employeeService;
        }

        /// <summary>
        /// Get the number of reports for a given employee.
        /// </summary>
        /// <param name="id">The EmployeeId for the employee to query reports for.</param>
        [HttpGet("{id}", Name = "getReportingStructureByEmployeeId")]
        public IActionResult GetReportStructure(string id) {
            Employee employee = this.employeeService.GetById(id);
            ReportingStructure report = new ReportingStructure(employee);
            return Ok(JsonConvert.SerializeObject(report));
        }
    }
}
