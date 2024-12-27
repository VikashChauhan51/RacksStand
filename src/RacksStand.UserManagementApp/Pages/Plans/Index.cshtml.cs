using Microsoft.AspNetCore.Mvc.RazorPages;
using RacksStand.Models;
using RacksStand.UnifiedServices.DataServices;

namespace RacksStand.UserManagementApp.Pages.Plans;

public class IndexModel(IPlanService planService) : PageModel
{
    public IEnumerable<PlanDetailsModel> Plans { get; set; } = [];
    public async Task OnGet()
    {
        Plans = await planService.GetPlansAsync();
    }
}
