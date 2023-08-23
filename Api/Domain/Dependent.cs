namespace Api.Domain
{
    /// <summary>
    /// Domain layer object representing a dependent.
    /// NOTE: added a domain layer to provide independent namesapce for
    /// communicating with the data layer.
    /// </summary>
    public class Dependent
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Relationship Relationship { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
