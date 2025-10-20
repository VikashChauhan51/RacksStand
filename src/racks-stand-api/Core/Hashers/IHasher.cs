namespace RacksStand.Api.Core.Hashers;

public interface IHasher
{
    string Hash(string text);
    bool VerifyHashed(string hashedText, string providedText);
}
