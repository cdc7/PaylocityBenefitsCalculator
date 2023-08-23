using Api.Models;
using Api.Repositories.Interfaces;
using Api.Services.Interfaces;
using AutoMapper;

namespace Api.Services
{
    /// <summary>
    /// Service layer class that can house various emplyee related methods.
    /// Seperated from reposirtory layer so that if any business logic needs
    /// to be done to the API models, it can be seperate from repository layer
    /// that handles domain objects.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper) 
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var domainEmployees = await _employeeRepository.GetAllEmployees();

            return _mapper.Map<List<Employee>>(domainEmployees);
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            var domainEmployee = await _employeeRepository.GetEmployeeById(id);

            return _mapper.Map<Employee?>(domainEmployee);
        }
    }
}
