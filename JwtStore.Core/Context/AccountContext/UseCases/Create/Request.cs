namespace JwtStore.Core.Context.AccountContext.UseCases.Create;

public class Request
{
    protected Request()
    {
        
    }
    public Request(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}