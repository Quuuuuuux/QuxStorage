namespace DomainLib.Entities;

public class Credentials : BaseEntity 
{
    public Credentials(){}

    #region Plain

    public string Login { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;

    #endregion
}