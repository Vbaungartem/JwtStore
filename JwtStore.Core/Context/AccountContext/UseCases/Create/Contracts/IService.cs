using JwtStore.Core.Context.AccountContext.Entities;

namespace JwtStore.Core.Context.AccountContext.UseCases.Create.Contracts;

public interface IService
{
    Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
}