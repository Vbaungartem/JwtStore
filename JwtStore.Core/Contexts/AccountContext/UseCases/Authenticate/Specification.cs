using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Password.Length, 40, "Password", "A senha deve conter ao máximo que 40 caracteres.")
            .IsGreaterThan(request.Password.Length, 8, "Password", "A senha deve conter ao menos 8 caracteres.")
            .IsEmail(request.Email, "Email", "E-mail inválido.");
}