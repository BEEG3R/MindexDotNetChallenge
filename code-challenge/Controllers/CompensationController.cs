using challenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace challenge.Controllers
{
    public class CompensationController : Controller
    {
        [HttpPost]
        public IActionResult CreateEmployeeCompensation([FromBody] Compensation compensation) {
            return View();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployeeCompensation(string id) {
            return View();
        }
    }
}
