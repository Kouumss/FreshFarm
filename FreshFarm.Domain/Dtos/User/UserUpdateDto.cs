 using System.ComponentModel.DataAnnotations;

namespace FreshFarm.Domain.Dtos.User;


public class UserUpdateDto

{   
    public Guid Id { get; set; } 

    [Display(Name = "Prénom")]
    public string? FirstName { get; set; }

    [Display(Name = "Nom de famille")]
    public string? LastName { get; set; }

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [DataType(DataType.Password)]
     [Display(Name = "Mot de passe")]
    public string? Password { get; set; }
}

