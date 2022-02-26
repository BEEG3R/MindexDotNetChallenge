using System;
using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller {
        private readonly ICompensationService compensationService;
        private const string GET_ROUTE = "getEmployeeCompensationById";

        public CompensationController(ICompensationService compensationService) {
            this.compensationService = compensationService;
        }

        /// <summary>
        /// Get the Compensation record for a given employee.
        /// </summary>
        /// <param name="id">The EmployeeId of the employee to search for compensation for.</param>
        [HttpGet("{id}", Name = GET_ROUTE)]
        public IActionResult GetEmployeeCompensation(string id) {
            // If we didn't receive an ID, just return 400 BadRequest before
            // trying to query the database.
            if (string.IsNullOrEmpty(id)) {
                return BadRequest();
            }
            Compensation compensation = this.compensationService.GetByEmployeeId(id);
            // If we queried the database and still didn't find a result,
            // return 404 Not Found.
            if (compensation == null) {
                return NotFound();
            }

            // If we get here, that means we received an ID, and it was valid
            // since we got a non-null database query result. Convert the
            // compensation object to JSON and return it with a 200 OK status.
            return Ok(JsonConvert.SerializeObject(compensation));
        }

        /// <summary>
        /// Create a new compensation record for a given employee.
        /// </summary>
        /// <param name="compensation">The compensation object to store in the database.</param>
        [HttpPost(Name = "createEmployeeCompensationRecord")]
        public IActionResult CreateEmployeeCompensation([FromBody] Compensation compensation) {
            // If we didn't receive an employee, just return 400 BadRequest
            // before trying to create a new record in the database.
            if (compensation == null) {
                return BadRequest();
            }
            
            // Store our new record in the database.
            this.compensationService.Create(compensation);
            // If we get here, the record was created and saved to the database. Return 201 Created.
            return CreatedAtRoute(GET_ROUTE, new {id = compensation.Employee.EmployeeId}, compensation);
        }
    }
}
