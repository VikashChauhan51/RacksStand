using System.ComponentModel.DataAnnotations;

namespace RacksStand.App.Features.Shops.Models;

public class ShopModel
{
    [Required(ErrorMessage = "Shop Name is required.")]
    [MaxLength(150, ErrorMessage = "Shop Name cannot be longer than 150 characters.")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [MaxLength(300, ErrorMessage = "Address cannot be longer than 300 characters.")]
    public string Address { get; set; } = string.Empty;
}
