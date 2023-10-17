using JwtStore.Core.Contexts.AccountContext.Entities;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create.Interfaces;

public interface IService
{
    Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
}