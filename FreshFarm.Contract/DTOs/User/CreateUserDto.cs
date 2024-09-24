using System;
using System.ComponentModel.DataAnnotations;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Contract.DTOs.User;

public class CreateUserDto
{
    [Required(ErrorMessage = "Le prénom est requis.")]
    [Display(Name = "Prénom")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Le nom de famille est requis.")]
    [Display(Name = "Nom de famille")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Le mot de passe est requis.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mot de passe")]
    public string Password { get; set; }

    [Required(ErrorMessage = "L'email est requis.")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; set; }
}
