namespace Api.Models
{
    /// <summary>
    /// Api model to represent an Employee paycheck.
    /// </summary>
    public class EmployeePaycheck
    {
        public decimal GrossPay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay 
        {
            get
            {
                return GrossPay - Deductions;
            }
        }
    }
}
