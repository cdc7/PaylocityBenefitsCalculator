namespace Api.Domain
{
    /// <summary>
    /// Domain layer enum representing a relationship.
    /// NOTE: added a domain layer to provide independent namesapce for
    /// communicating with the data layer.
    /// </summary>
    public enum Relationship
    {
        None,
        Spouse,
        DomesticPartner,
        Child
    }
}
