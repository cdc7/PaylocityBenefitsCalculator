namespace Api.Models;

public class Dependent
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    // added property with custom get to calculate a dpendent age at any time
    public int Age
    {
        get
        {
            var today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.Date.AddYears(-age))
                age--;

            return age;
        }
    }
}
