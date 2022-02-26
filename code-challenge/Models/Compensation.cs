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
    }
}
