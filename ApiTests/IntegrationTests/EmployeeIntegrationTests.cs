using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.IntegrationTests;


/// <summary>
/// Integration tests for EmployeeController
/// NOTE: for an issue related to my laptop or network, I was not able to
/// test with the <see cref="IntegrationTest"/> class. I was receiving an HttpSocket
/// exception and was unclear how to resolve. To get past this issue I implemented
/// the integration test appraoch documented here: https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0#basic-tests-with-the-default-webapplicationfactory
/// </summary>
public class EmployeeIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public EmployeeIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost:7124")
        });
    }

    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        var response = await _httpClient.GetAsync("/api/v1/employees");
        var employees = new List<GetEmployeeDto>
        {
            new()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3)
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
                    }
                }
            },
            new()
            {
                Id = 3,
                FirstName = "Michael",
                LastName = "Jordan",
                Salary = 143211.12m,
                DateOfBirth = new DateTime(1963, 2, 17),
                Dependents = new List<GetDependentDto>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2)
                    }
                }
            }
        };
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        var response = await _httpClient.GetAsync("/api/v1/employees/1");
        var employee = new GetEmployeeDto
        {
            Id = 1,
            FirstName = "LeBron",
            LastName = "James",
            Salary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30)
        };
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }
    
    [Fact]
    //task: make test pass
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await _httpClient.GetAsync($"/api/v1/employees/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

