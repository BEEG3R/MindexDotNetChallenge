using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using challenge.Data;
using challenge.Models;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository {
        private readonly EmployeeContext employeeContext;

        public CompensationRepository(EmployeeContext employeeContext) {
            this.employeeContext = employeeContext;
        }

        public Compensation Add(Compensation compensation) {
            this.employeeContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation GetByEmployeeId(string id) {
            return this.employeeContext.Compensations.FirstOrDefault(c => c.Employee.EmployeeId == id);
        }

        public Task SaveAsync() {
            return this.employeeContext.SaveChangesAsync();
        }
    }
}
