using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RacksStand.Models;

namespace RacksStand.UserManagementApp.Pages.Onboarding;

public class IndexModel(IPlanService planService) : PageModel
{

    [BindProperty(SupportsGet = true)]
    public string? PlanId { get; set; }

    [BindProperty]
    public PlanDetailsModel? Plan { get; set; }
    public async Task OnGet()
    {
        if (!string.IsNullOrEmpty(PlanId))
        {
            var response = await planService.GetPlanAsync(PlanId);
            Plan = response.Match
            (
               data => data,
               _ => null!
            );
        }
    }

    public IActionResult OnPost()
    {
        return RedirectToPage("/Signup", new { PlanId = PlanId });
      
    }
}
