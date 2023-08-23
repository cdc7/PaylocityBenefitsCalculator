using Api.Dtos.Employee;
using Api.Models;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

/// <summary>
/// Additional controller added for retrieving employee checks.
/// Was needed since check retirval is a get method with an id parameter
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class EmployeePaycheckController : ControllerBase
{
    private readonly IEmployeePaycheckService _employeePaycheckService;
    private readonly IMapper _mapper;

    public EmployeePaycheckController(IMapper mapper, IEmployeePaycheckService employeePaycheckService)
    {
        _mapper = mapper;
        _employeePaycheckService = employeePaycheckService;
    }

    [SwaggerOperation(Summary = "Get employee check by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeePaycheckDto>>> GetCheckByEmployeeId(int id)
    {
        var employeeCheck = await _employeePaycheckService.GetEmployeePaycheckByEmployeeId(id);
        var getEmployeePaycheckDto = _mapper.Map<GetEmployeePaycheckDto>(employeeCheck);

        //use NotFound method to ensure correct HTTPS status code with helpful error message
        if (getEmployeePaycheckDto == null)
            return NotFound(new ApiResponse<GetEmployeePaycheckDto>
            {
                Data = getEmployeePaycheckDto,
                Success = false,
                Message = "There was an error retrieving Employee check",
                Error = $"Employee with id: {id} not found"
            });

        var result = new ApiResponse<GetEmployeePaycheckDto>
        {
            Data = getEmployeePaycheckDto,
            Success = true
        };

        return result;
    }
}
