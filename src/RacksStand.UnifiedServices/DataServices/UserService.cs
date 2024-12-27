using Microsoft.AspNetCore.Identity;

namespace RacksStand.UnifiedServices.DataServices;
public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IPlanService _planService;
    private readonly IRepository<UserPlan> _userPlanRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserService> _logger;

    public UserService
    (
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IPlanService planService,
        IRepository<UserPlan> userPlanRepository,
        IUnitOfWork unitOfWork,
        ILogger<UserService> logger
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _planService = planService;
        _userPlanRepository = userPlanRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<string>> RegisterAsync(SignupModel signup, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Onboarding a user {Email}", signup.Email);

        var plan = await _planService.GetPlanAsync(signup.PlanId, cancellationToken);
        if (!plan.IsSuccess)
        {
            return Result<string>.Fail(new EntityNotFoundException("Plan not found"));
        }
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = signup.Email,
            Email = signup.Email,
            PhoneNumber = signup.PhoneNumber,
            FullName = signup.FullName,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };
        var result = await _userManager.CreateAsync(user, signup.Password);
        if (!result.Succeeded)
        {
            return Result<string>.Fail(new Exception(string.Join(" ", result.Errors.Select(x => x.Description))));
        }
        var userPlan = new UserPlan
        {
            Id = UserPlanId.Of(Guid.NewGuid().ToString()),
            PlanId = PlanId.Of(signup.PlanId),
            UserId = user.Id,
            StartDate = DateTime.UtcNow,
            IsActive = true
        };
        await _userPlanRepository.AddAsync(userPlan, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return Result<string>.Succ(user.Id);
    }
    public async Task<Result<bool>> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result<bool>.Fail(new Exception("user not found."));
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);

        if (result == null)
        {
            return Result<bool>.Fail(new Exception("user not found."));
        }
        if (!result.Succeeded)
        {
            return Result<bool>.Fail(new Exception("user account is not active."));
        }
        return Result<bool>.Succ(true);
    }

    public async Task<Result<bool>> ResetPasswordAsync(string email, string newPassword, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result<bool>.Fail(new Exception("user not found."));
        }

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        if (result == null)
        {
            return Result<bool>.Fail(new Exception("user not found."));
        }
        if (!result.Succeeded)
        {
            return Result<bool>.Fail(new Exception("user account is not active."));
        }
        return Result<bool>.Succ(true);
    }

    public async Task<Result<string>> GeneratePasswordResetTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result<string>.Fail(new Exception("user not found."));
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        return Result<string>.Succ(token);
    }

    public async Task<Result<User>> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result<User>.Fail(new Exception("user not found."));
        }
        return Result<User>.Succ(user);
    }
    public async Task<Result<bool>> ChangePasswordAsync(User user, string currentPassword, string newPassword)
    {
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (result == null)
        {
            return Result<bool>.Fail(new Exception("user not found."));
        }
        if (!result.Succeeded)
        {
            return Result<bool>.Fail(new Exception(string.Join(" ", result.Errors.Select(x => x.Description))));
        }
        return Result<bool>.Succ(true);
    }
}
