using Api.Models;

namespace Api.Services.Interfaces
{
    public interface IEmployeePaycheckService
    {
        /// <summary>
        /// Calculates a paycheck for an employee. Includes gross pay and deduction calculations.
        /// </summary>
        /// <param name="id">Id of the employee to calculate check for.</param>
        /// <returns>A <see cref="EmployeePaycheck"/> if employee is found, null if not.</returns>
        Task<EmployeePaycheck?> GetEmployeePaycheckByEmployeeId(int id);

        /// <summary>
        /// Calculates the employee only deductions, independent from dependednts.
        /// </summary>
        /// <param name="employeeSalary">Salary of the employee.</param>
        /// <returns>A <see cref="decimal"/> value of employee only deductions.</returns>
        decimal CalculateEmployeeOnlyDeductions(decimal employeeSalary);

        /// <summary>
        /// Calculates an dependent's deductions.
        /// </summary>
        /// <param name="dependents">Dependent collection to calculate deductions for.</param>
        /// <returns>A <see cref="decimal"/> value of dependent deductions.</returns>
        public decimal CalculateDependentDeductions(ICollection<Dependent> dependents);
    }
}
