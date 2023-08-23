using Api.Models;

namespace Api.Services.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Calls repository to retrieve list of all employees.
        /// </summary>
        /// <returns>List of <see cref="Dependent"/> objects.</returns>
        Task<List<Employee>> GetAllEmployees();

        /// <summary>
        /// Calls repository to retrieve employee by id.
        /// </summary>
        /// <param name="id">Id to search.</param>
        /// <returns>A <see cref="Employee"/> object if found, null if not.</returns>
        Task<Employee?> GetEmployeeById(int id);
    }
}
