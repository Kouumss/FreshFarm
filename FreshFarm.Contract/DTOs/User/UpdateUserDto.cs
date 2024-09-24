using System.ComponentModel.DataAnnotations;

namespace FreshFarm.Contract.DTOs.User;


public class UpdateUserDto
{
    [Display(Name = "Prénom")]
    public string? FirstName { get; set; }

    [Display(Name = "Nom de famille")]
    public string? LastName { get; set; }

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string? Email { get; set; }
}

