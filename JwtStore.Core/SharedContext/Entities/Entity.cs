using System.Data.Common;

namespace JwtStore.Core.SharedContext.Entities;

public class Entity : IEquatable<Guid>
{
    protected Entity() => Id = Guid.NewGuid();

    public Guid Id {get;}

    public bool Equals(Guid id)
       => Id == id;

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}