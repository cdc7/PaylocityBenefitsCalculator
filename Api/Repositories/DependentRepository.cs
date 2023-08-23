using Api.Domain;
using Api.Repositories.Interfaces;

namespace Api.Repositories
{

    /// <summary>
    /// Added repository implementation to separate the concern of
    /// data retrieval from the service layer. In a prod envronment,
    /// this repository class could use a Entity Framework context to
    /// query the database rather that linq queries on a readonly list
    /// </summary>
    public class DependentRepository : IDependentRepository
    {
        private readonly List<Dependent> _dependentData = new()
        {
            new()
            {
                Id = 1,
                FirstName = "Spouse",
                LastName = "Morant",
                Relationship = Relationship.Spouse,
                DateOfBirth = new DateTime(1998, 3, 3),
            },
            new()
            {
                Id = 2,
                FirstName = "Child1",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2020, 6, 23)
            },
            new()
            {
                Id = 3,
                FirstName = "Child2",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2021, 5, 18)
            },
            new()
            {
                Id = 4,
                FirstName = "DP",
                LastName = "Jordan",
                Relationship = Relationship.DomesticPartner,
                DateOfBirth = new DateTime(1974, 1, 2)
            }
        };

        public Task<List<Dependent>> GetAllDependents()
        {
            return Task.FromResult(_dependentData);
        }

        public Task<Dependent?> GetDependentById(int id)
        {
            var dependent = _dependentData.FirstOrDefault(dep => dep.Id == id);
            return Task.FromResult(dependent);
        }
    }
}
