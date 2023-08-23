using Api.Dtos.Dependent;
using Api.Models;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentService _dependentService;
    private readonly IMapper _mapper;

    public DependentsController(IDependentService dependentService, IMapper mapper)
    {
        _dependentService = dependentService;
        _mapper = mapper;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dependent = await _dependentService.GetDependentById(id);
        var getDependentDto = _mapper.Map<GetDependentDto>(dependent);

        //use NotFound method to ensure correct HTTPS status code with helpful error message
        if (getDependentDto == null)
            return NotFound(new ApiResponse<GetDependentDto>
            {
                Data = getDependentDto,
                Success = false,
                Message = "There was an error retrieving Dependent data",
                Error = $"Dependent with id: {id} not found"
            });

        var result = new ApiResponse<GetDependentDto>
        {
            Data = getDependentDto,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = await _dependentService.GetAllDependents();
        var getDependentDtos = _mapper.Map<List<GetDependentDto>>(dependents);

        var result = new ApiResponse<List<GetDependentDto>>
        {
            Data = getDependentDtos,
            Success = true
        };

        return result;
    }
}
