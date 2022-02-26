#region

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace challenge.Models {
    public class Compensation {
        [Key] public int Id { get; set; }

        public Employee Employee { get; set; }

        public int Salary { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
