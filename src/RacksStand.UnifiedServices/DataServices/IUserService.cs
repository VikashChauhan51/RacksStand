
namespace RacksStand.UnifiedServices.DataServices;

public interface IUserService
{
    Task<Result<bool>> ChangePasswordAsync(User user, string currentPassword, string newPassword);
    Task<Result<string>> GeneratePasswordResetTokenAsync(string email);
    Task<Result<User>> GetUserByEmailAsync(string email);
    Task<Result<bool>> LoginAsync(string email, string password);
    Task<Result<string>> RegisterAsync(SignupModel signup, CancellationToken cancellationToken = default);
    Task<Result<bool>> ResetPasswordAsync(string email, string newPassword, string token);
}
