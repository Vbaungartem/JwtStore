using System.Text.RegularExpressions;
using JwtStore.Core.SharedContext.Extensions;
using JwtStore.Core.SharedContext.ValueObjects;

namespace JwtStore.Core.AcocuntContext.ValueObjects;

public partial class Email :ValueObject
{
    private const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

public Email(string address)
{
    if (string.IsNullOrEmpty(address))
        throw new Exception("E-mail inválido");

    Address = address.Trim().ToLower();

    if (Address.Length < 5)
        throw new Exception("E-mail inválido");

    if (!EmailRegex().IsMatch(Address))
        throw new Exception("E-mail inválido");
}

    public static implicit operator string(Email email)
        => email.ToString();

    public static implicit operator Email(string address)
        => new Email(address);
    public override string ToString()
        => Address; 

    public string Address {get;}
    public string Hash  => Address.ToBase64();
    public Verification Verification {get; private set;} = new();

    public void ResendVerification() => Verification = new Verification();

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex(); 
}