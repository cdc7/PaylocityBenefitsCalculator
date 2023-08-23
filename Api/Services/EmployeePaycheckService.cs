using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services
{
    /// <summary>
    /// Service to process pay check calculations. This logic was added as a service
    /// and separate from the controller to seperate concerns and to improve testablitiy.
    /// NOTE: Since check calculation invlolves division, this service
    /// would need to consider rounding correction, but since challenge
    /// only specified retrieving a single check this was omitted from the service
    /// </summary>
    public class EmployeePaycheckService : IEmployeePaycheckService
    {
        private readonly IEmployeeService _employeeService;

        // declare constants per the specification for calculating checks.
        // since there are 26 checks, declare constants with yearly values to
        // be calculated more easily per check
        private const decimal _salaryForBenefitsIncrease = 80000;
        private const decimal _benefitsIncreasePercentage = 0.02M;
        private const int _checksPerYear = 26;
        private const decimal _yearlyDefaultDeduction = 1000 * 12;
        private const decimal _yearlyDependentBenefitsIncrease = 600 * 12;
        private const decimal _yearlySeniorDependentBenefitsIncrease = 200 * 12;

        public EmployeePaycheckService(IEmployeeService employeeService) 
        {
            _employeeService = employeeService;
        }

        public async Task<EmployeePaycheck?> GetEmployeePaycheckByEmployeeId(int id)
        {
            var paycheck = new EmployeePaycheck();
            var employee = await _employeeService.GetEmployeeById(id);

            if (employee == null)
                return null;

            paycheck.GrossPay = Math.Round(employee.Salary/26, 2); // calculate gross pay

            //separate employee only deductions from dependent deductions in case
            //each value is needed if check needs more detail and for better testability
            var totalDeductions = CalculateEmployeeOnlyDeductions(employee.Salary) + CalculateDependentDeductions(employee.Dependents);
            paycheck.Deductions = Math.Round(totalDeductions, 2);

            return paycheck;
        }

        public decimal CalculateEmployeeOnlyDeductions(decimal employeeSalary)
        {
            decimal employeeOnlyDeductions = _yearlyDefaultDeduction / _checksPerYear;

            if (employeeSalary > _salaryForBenefitsIncrease)
            {
                var extraYearlyDeductions = employeeSalary * _benefitsIncreasePercentage;
                employeeOnlyDeductions += (extraYearlyDeductions / _checksPerYear);
            }

            return employeeOnlyDeductions;
        }

        public decimal CalculateDependentDeductions(ICollection<Dependent> dependents)
        {
            decimal dependentDeductions = 0;

            foreach (var dependent in dependents)
            {
                dependentDeductions += (_yearlyDependentBenefitsIncrease / _checksPerYear);
                if (dependent.Age > 50)
                    dependentDeductions += (_yearlySeniorDependentBenefitsIncrease / _checksPerYear);
            }

            return dependentDeductions;
        }
    }
}
