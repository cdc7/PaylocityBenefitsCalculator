using Api.Models;
using Api.Services;
using Api.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.UnitTests
{
    /// <summary>
    /// Unit test file to test indivdual compents of check service
    /// expected out values used to assert test cases were done by hand as
    /// I understood the prompt
    /// </summary>
    public class EmployeePayCheckServiceUnitTests
    {
        // used the Moq library to mock the services needed within the methods
        // that are called in this file
        private readonly Mock<IEmployeeService> _employeeServicesMock;
        private readonly List<Employee> _employeeData = new List<Employee>
        {
            new ()
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Salary = 75420.99m,
                DateOfBirth = new DateTime(1984, 12, 30)
            },
            new ()
            {
                Id = 2,
                FirstName = "Ja",
                LastName = "Morant",
                Salary = 92365.22m,
                DateOfBirth = new DateTime(1999, 8, 10),
                Dependents = new List<Dependent>
                {
                    new ()
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
                Dependents = new List<Dependent>
                {
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship = Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1972, 1, 2) //increased age by two yeard to cover over 50 rule
                    }
                }
            }
        };

        public EmployeePayCheckServiceUnitTests()
        {
            _employeeServicesMock = new Mock<IEmployeeService>();
        }

        [Fact]
        public async Task WhenAskedForCheckOfNonExistentEmployee_ReturnNull()
        {
            //arrange
            _employeeServicesMock
                .Setup(employeeService => employeeService.GetEmployeeById(int.MinValue))
                .ReturnsAsync((Employee?)null);

            var employeePaycheckService = new EmployeePaycheckService(_employeeServicesMock.Object);

            //act
            var result = await employeePaycheckService.GetEmployeePaycheckByEmployeeId(int.MinValue);

            //assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, 2439.27)]
        [InlineData(2, 2189.15)]
        [InlineData(3, 4567.19)]
        public async Task WhenAskedForCheckOfEmployee_CheckIsCorrect(int employeeId, decimal expectedNetPay)
        {
            //arrange
            _employeeServicesMock
                .Setup(employeeService => employeeService.GetEmployeeById(employeeId))
                .ReturnsAsync(_employeeData.FirstOrDefault(e => e.Id == employeeId));

            var employeePaycheckService = new EmployeePaycheckService(_employeeServicesMock.Object);

            //act
            var result = await employeePaycheckService.GetEmployeePaycheckByEmployeeId(employeeId);

            //assert
            Assert.NotNull(result);
            Assert.Equal(result.NetPay, expectedNetPay);
        }

        [Theory]
        [InlineData(1, 461.54)]
        [InlineData(2, 532.59)]
        [InlineData(3, 571.70)]
        public void WhenAskedForCheckOfEmployee_EmployeeOnlyDeductionsCorrect(int employeeId, decimal expectedDedctions)
        {
            //arrange
            var employeeSalary = _employeeData.Single(e => e.Id == employeeId).Salary;

            var employeePaycheckService = new EmployeePaycheckService(_employeeServicesMock.Object);

            //act
            var result = employeePaycheckService.CalculateEmployeeOnlyDeductions(employeeSalary);

            //assert
            Assert.Equal(Math.Round(result, 2), expectedDedctions);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 830.77)]
        [InlineData(3, 369.23)]
        public void WhenAskedForCheckOfEmployee_DependentDeductionsCorrect(int employeeId, decimal expectedDeductions)
        {
            //arrange
            var dependents = _employeeData.Single(e => e.Id == employeeId).Dependents;

            var employeePaycheckService = new EmployeePaycheckService(_employeeServicesMock.Object);

            //act
            var result = employeePaycheckService.CalculateDependentDeductions(dependents);

            //assert
            Assert.Equal(Math.Round(result, 2), expectedDeductions);
        }
    }
}
