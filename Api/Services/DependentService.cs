using Api.Models;
using Api.Repositories.Interfaces;
using Api.Services.Interfaces;
using AutoMapper;

namespace Api.Services
{
    /// <summary>
    /// Service layer class that can house various dependent related methods.
    /// Seperated from reposirtory layer so that if any business logic needs
    /// to be done to the API models, it can be seperate from repository layer
    /// that handles domain objects.
    /// </summary>
    public class DependentService : IDependentService
    {
        private readonly IDependentRepository _dependentRepository;
        private readonly IMapper _mapper;

        public DependentService(IDependentRepository dependentRepository, IMapper mapper) 
        {
            _dependentRepository = dependentRepository;
            _mapper = mapper;
        }

        public async Task<List<Dependent>> GetAllDependents()
        {
            var domainDependents = await _dependentRepository.GetAllDependents();

            return _mapper.Map<List<Dependent>>(domainDependents);
        }

        public async Task<Dependent?> GetDependentById(int id)
        {
            var domainDependent = await _dependentRepository.GetDependentById(id);

            return _mapper.Map<Dependent?>(domainDependent);
        }
    }
}
