using Api.Dtos.Dependent;
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
/// Integration tests for DependentController
/// NOTE: for an issue related to my laptop or network, I was not able to
/// test with the <see cref="IntegrationTest"/> class. I was receiving an HttpSocket
/// exception and was unclear how to resolve. To get past this issue I implemented
/// the integration test appraoch documented here: https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0#basic-tests-with-the-default-webapplicationfactory
/// </summary>
public class DependentIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public DependentIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost:7124")
        });
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForAllDependents_ShouldReturnAllDependents()
    {
        var response = await _httpClient.GetAsync("/api/v1/dependents");
        var dependents = new List<GetDependentDto>
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
        await response.ShouldReturn(HttpStatusCode.OK, dependents);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForADependent_ShouldReturnCorrectDependent()
    {
        var response = await _httpClient.GetAsync("/api/v1/dependents/1");
        var dependent = new GetDependentDto
        {
            Id = 1,
            FirstName = "Spouse",
            LastName = "Morant",
            Relationship = Relationship.Spouse,
            DateOfBirth = new DateTime(1998, 3, 3)
        };
        await response.ShouldReturn(HttpStatusCode.OK, dependent);
    }

    [Fact]
    //task: make test pass
    public async Task WhenAskedForANonexistentDependent_ShouldReturn404()
    {
        var response = await _httpClient.GetAsync($"/api/v1/dependents/{int.MinValue}");
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}

