using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using AutoMapper;

namespace Api.MappingProfiles
{
    /// <summary>
    /// Mapping profile for AutoMapper mappings.
    /// Added to aid in the conversion between object in different project layers.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Employee, Models.Employee>();
            CreateMap<Domain.Dependent, Models.Dependent>();
            CreateMap<Models.Employee, GetEmployeeDto>();
            CreateMap<Models.Dependent, GetDependentDto>();
            CreateMap<Models.EmployeePaycheck, GetEmployeePaycheckDto>();
        }
    }
}
