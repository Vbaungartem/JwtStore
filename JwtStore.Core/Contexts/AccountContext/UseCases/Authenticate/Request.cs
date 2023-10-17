using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Request : IRequest<Response>
{
    protected Request()
    {
    }
    public Request(string email, string password)
    {
        Email = email;
        Password = password;
    }
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}