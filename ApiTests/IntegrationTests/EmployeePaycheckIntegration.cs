using Api.Dtos.Employee;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.IntegrationTests;

/// <summary>
/// Integration tests for EmployeePaycheckController
/// NOTE: for an issue related to my laptop or network, I was not able to
/// test with the <see cref="IntegrationTest"/> class. I was receiving an HttpSocket
/// exception and was unclear how to resolve. To get past this issue I implemented
/// the integration test appraoch documented here: https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0#basic-tests-with-the-default-webapplicationfactory
/// </summary>
public class EmployeePaycheckIntegration : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public EmployeePaycheckIntegration(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost:7124")
        });
    }

    [Theory]
    [InlineData(1, 2900.81, 461.54)]
    [InlineData(2, 3552.51, 1363.36)]
    [InlineData(3, 5508.12, 848.62)]
    public async Task WhenAskedForEmployeePayCheck_ShouldReturnWithRightAmounts(int employeeId, decimal expectedGrossPay, decimal expectedDeductions)
    {
        var response = await _httpClient.GetAsync($"/api/v1/employeepaycheck/{employeeId}");
        var employees = new GetEmployeePaycheckDto
        {
            GrossPay = expectedGrossPay,
            Deductions = expectedDeductions,
            NetPay = expectedGrossPay - expectedDeductions
        };
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }
    
    [Fact]
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var response = await _httpClient.GetAsync($"/api/v1/employeepaychecks/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

