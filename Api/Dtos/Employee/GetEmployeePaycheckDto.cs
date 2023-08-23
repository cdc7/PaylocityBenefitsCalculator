namespace Api.Dtos.Employee
{
    /// Added dto for the employee paychecks controller
    public class GetEmployeePaycheckDto
    {
        public decimal GrossPay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay { get; set; }
    }
}
