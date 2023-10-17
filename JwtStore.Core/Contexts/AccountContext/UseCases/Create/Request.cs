using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Request : IRequest<Response>
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