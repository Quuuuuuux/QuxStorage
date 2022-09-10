namespace DomainLib.Entities;

public class CredentialsPair : BaseEntity 
{
    public CredentialsPair(){}

    #region Plain

    public string Login { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;

    #endregion
}