using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RacksStand.Models;
using RacksStand.UnifiedServices.DataServices;

namespace RacksStand.UserManagementApp.Pages;

public class SignupModel(IPlanService planService, IUserService userService) : PageModel
{
 

    [BindProperty]
    public PlanDetailsModel Plan { get; set; }

    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }
    public async Task OnGet(string planId)
    {
        if (!string.IsNullOrEmpty(planId))
        {
            var response = await planService.GetPlanAsync(planId);
            Plan = response.Match
            (
               data => data,
               _ => null!
            );
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {

        var result = await userService.RegisterAsync(new Models.SignupModel
        (
        FullName: "Vikash Chauhan",
        Email: "Vikashchauhan51@gmail.com",
        PhoneNumber: "7355093955",
        Password: "Welcome@123",
        PlanId: Plan.Id
        ));
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(string.Empty, "There was an error creating your account.");
            return Page();
        }
        if (Plan.Price > 0)
        {
            return RedirectToPage("/Payment", new { planId = Plan.Id, userId = result.Value });
        }
        return RedirectToPage("/Index");
    }
}
