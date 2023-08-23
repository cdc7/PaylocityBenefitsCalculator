using Api.Domain;

namespace Api.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Retrieves all employees from datasource.
        /// </summary>
        /// <returns>A list of <see cref="Employee"/> objects.</returns>
        Task<List<Employee>> GetAllEmployees();

        /// <summary>
        /// Retrieves an employee by id.
        /// </summary>
        /// <param name="id">Id of employee to retrieve.</param>
        /// <returns>A <see cref="Employee"/> if found, null if not.</returns>
        Task<Employee?> GetEmployeeById(int id);
    }
}
