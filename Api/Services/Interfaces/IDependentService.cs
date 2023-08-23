using Api.Models;

namespace Api.Services.Interfaces
{
    public interface IDependentService
    {
        /// <summary>
        /// Calls repository to retrieve list of all dependents.
        /// </summary>
        /// <returns>List of <see cref="Dependent"/> objects.</returns>
        Task<List<Dependent>> GetAllDependents();

        /// <summary>
        /// Calls repository to retrieve dependent by id.
        /// </summary>
        /// <param name="id">Id to search.</param>
        /// <returns>A <see cref="Dependent"/> object if found, null if not.</returns>
        Task<Dependent?> GetDependentById(int id);
    }
}
