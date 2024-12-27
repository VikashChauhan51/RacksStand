using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RacksStand.Models;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;
using RacksStand.UnifiedServices.DataServices;

namespace RacksStand.UserManagementApp.Pages;

public class SignupModel(IPlanService planService) : PageModel
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
        // Handle the signup process
        var user = new User
        {
            Name = Name,
            Email = Email,
            Password = Password
        };

        var result = await _userService.CreateUserAsync(user);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, "There was an error creating your account.");
            return Page();
        }

        // If the plan is paid, redirect to the payment page
        if (Plan.Price > 0)
        {
            return RedirectToPage("/Payment", new { planId = Plan.Id });
        }

        // Otherwise, complete the signup process
        return RedirectToPage("/Home");  // Redirect to the home page or dashboard
    }
}
