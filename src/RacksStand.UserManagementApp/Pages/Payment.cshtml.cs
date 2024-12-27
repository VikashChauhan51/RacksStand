using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RacksStand.Models;
using RacksStand.Persistence.ValueObjects;
using RacksStand.UnifiedServices.DataServices;

namespace RacksStand.UserManagementApp.Pages;

public class PaymentModel(IPlanService planService) : PageModel
{
    [BindProperty]
    public PlanDetailsModel Plan { get; set; }

    [BindProperty]
    public string CreditCardNumber { get; set; }

    [BindProperty]
    public string ExpirationDate { get; set; }

    [BindProperty]
    public string Cvc { get; set; }
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
        // Process payment
        var paymentResult = await _paymentService.ProcessPaymentAsync(CreditCardNumber, ExpirationDate, Cvc, Plan.Price);
        if (!paymentResult.Success)
        {
            ModelState.AddModelError(string.Empty, "Payment failed. Please try again.");
            return Page();
        }

        // On success, complete the signup and redirect to the dashboard
        return RedirectToPage("/Home");
    }
}
