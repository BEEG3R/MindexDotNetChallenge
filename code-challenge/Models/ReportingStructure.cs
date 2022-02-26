using System.Collections.Generic;

namespace challenge.Models
{
    public class ReportingStructure {
        private int numberOfReports;

        /// <summary>
        /// Create a new ReportingStructure for a given Employee.
        /// </summary>
        /// <param name="employee">The Employee object to create a new structure for.</param>
        public ReportingStructure(Employee employee) {
            this.Employee = employee;
        }

        public Employee Employee { get; }

        public int NumberOfReports {
            get {
                CalculateNumberOfReports();
                return this.numberOfReports;
            }
            private set {
                this.numberOfReports = value;
            }
        }

        /// <summary>
        /// For the Employee reference in this object, count *all* direct
        /// reports for that employee. An employee that reports to a direct
        /// report of the current employee is considered a direct report
        /// of the current employee, and so on.
        /// </summary>
        private void CalculateNumberOfReports() {
            // If the employee has no direct reports, the list will be null.
            // To avoid checking properties of a null, set the number of
            // reports to 0 and return.
            if (this.Employee.DirectReports == null) {
                this.numberOfReports = 0;
                return;
            }
            // If the employee does have direct reports, record the count
            this.numberOfReports += this.Employee.DirectReports.Count;
            // recurse, tallying reports for direct reports of direct reports, and so on.
            // this method of recursion does not need the "tree" structure to be evenly distributed.
            foreach (Employee e in this.Employee.DirectReports) {
                this.numberOfReports += new ReportingStructure(e).NumberOfReports;
            }
        }
    }
}
