using Api.Domain;

namespace Api.Repositories.Interfaces
{
    public interface IDependentRepository
    {
        /// <summary>
        /// Retrieves all dependents from datasource.
        /// </summary>
        /// <returns>A list of <see cref="Dependent"/> objects.</returns>
        Task<List<Dependent>> GetAllDependents();

        /// <summary>
        /// Retrieves a dependent by id.
        /// </summary>
        /// <param name="id">Id of dependent to retrieve.</param>
        /// <returns>A <see cref="Dependent"/> if found, null if not.</returns>
        Task<Dependent?> GetDependentById(int id);
    }
}
