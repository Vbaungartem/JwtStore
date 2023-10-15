using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AcocuntContext.ValueObjects;

public class Verification : ValueObject
{
    public string Code {get;} = Guid.NewGuid().ToString("N")[..6].ToUpper();
    public DateTime? ExpiresAt {get; private set;} = DateTime.UtcNow.AddMinutes(5);
    public DateTime? VerifiedAt {get; private set;} = null;
    public bool isActive => VerifiedAt != null && ExpiresAt == null;


    public void Verify(string code)
    {
        if(isActive)
            throw new Exception("Este código já foi utilizado.");

        if(ExpiresAt < DateTime.UtcNow)
            throw new Exception("Código expirado.");

        if(!string.Equals(code.Trim(), Code.Trim(), StringComparison.CurrentCulture))
            throw new Exception("Código inválido.");


        ExpiresAt = null;
        VerifiedAt = DateTime.UtcNow;
    }
}