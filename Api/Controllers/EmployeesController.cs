using Api.Dtos.Employee;
using Api.Models;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeService employeeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var employee = await _employeeService.GetEmployeeById(id);
        var getEmployeeDto = _mapper.Map<GetEmployeeDto>(employee);

        //use NotFound method to ensure correct HTTPS status code with helpful error message
        if (getEmployeeDto == null)
            return NotFound(new ApiResponse<GetEmployeeDto>
            {
                Data = getEmployeeDto,
                Success = false,
                Message = "There was an error retrieving Employee data",
                Error = $"Employee with id: {id} not found"
            });

        var result = new ApiResponse<GetEmployeeDto>
        {
            Data = getEmployeeDto,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: use a more realistic production approach
        //approach I took was implemening a service layer that interacts with
        //repository layer that has direct access to data
        var employees = await _employeeService.GetAllEmployees();
        var getEmployeeDtos = _mapper.Map<List<GetEmployeeDto>>(employees);

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = getEmployeeDtos,
            Success = true
        };

        return result;
    }
}
