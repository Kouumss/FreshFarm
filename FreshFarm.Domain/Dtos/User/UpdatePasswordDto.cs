using System.ComponentModel.DataAnnotations;

namespace FreshFarm.Domain.Dtos.User;

public class UpdatePasswordDto
{
    [Required(ErrorMessage = "Le mot de passe actuel est requis.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mot de passe actuel")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "Le nouveau mot de passe est requis.")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "Le mot de passe doit comporter au moins {2} caractères.", MinimumLength = 6)]
    [Display(Name = "Nouveau mot de passe")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "La confirmation du mot de passe est requise.")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Le mot de passe et la confirmation doivent correspondre.")]
    [Display(Name = "Confirmer nouveau mot de passe")]
    public string ConfirmPassword { get; set; }
}
