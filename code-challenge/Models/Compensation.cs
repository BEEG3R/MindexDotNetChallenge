using System;

namespace challenge.Models
{
    public class Compensation {
        private Employee employee;
        private int salary;
        private DateTime effectiveDate;

        public Compensation(Employee employee, int salary, DateTime effectiveDate) {
            this.employee = employee;
            this.salary = salary;
            this.effectiveDate = effectiveDate;
        }

        public string EmployeeId {
            get {
                return this.employee.EmployeeId;
            }
        }

        public int Salary {
            get {
                return this.salary;
            }
        }

        public DateTime EffectiveDate {
            get {
                return this.effectiveDate;
            }
        }
    }
}
