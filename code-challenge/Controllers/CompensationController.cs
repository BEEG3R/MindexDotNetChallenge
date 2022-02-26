using System;
using System.Net;
using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller {
        private readonly ICompensationService compensationService;

        public CompensationController(ICompensationService compensationService) {
            this.compensationService = compensationService;
        }

        [HttpGet("{id}", Name = "getEmployeeCompensationById")]
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

        [HttpPost(Name = "createEmployeeCompensationRecord")]
        public IActionResult CreateEmployeeCompensation([FromBody] Employee employee, 
                                                        [FromBody] int salary, 
                                                        [FromBody] DateTime effectiveDate) {
            return View();
        }
    }
}
