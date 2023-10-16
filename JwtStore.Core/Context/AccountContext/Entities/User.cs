using JwtStore.Core.Context.AcocuntContext.ValueObjects;
using JwtStore.Core.Context.SharedContext.Entities;

namespace JwtStore.Core.Context.AccountContext.Entities;

public class User : Entity
{

    protected User()
    {
    }

    public User(Email email, string? password = null)
    {
        Email = email;
        Password = new Password(password);
    }
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public string Image { get; private set; } = string.Empty;

    public void UpdatePassword(string plainTextPassword, string code)
    {
        if(!String.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception ("Código inválido");

        var password = new Password(plainTextPassword);
        Password = password;
    } 

    public void UpdateEmail(Email email)
    {
        Email = email;
    }

    public void ChangePassword(string plainTextPassword)
    {
        var password = new Password(plainTextPassword);
        Password = password;
    }
}