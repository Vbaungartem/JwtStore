using JwtStore.Core.AcocuntContext.ValueObjects;
using JwtStore.Core.SharedContext.Entities;

namespace JwtStore.Core.AcocuntContext.Entities;

public class User : Entity
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
}