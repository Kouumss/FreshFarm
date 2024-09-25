using FreshFarm.Application.Error;
using FreshFarm.Domain.Dtos.User;
using FreshFarm.UI.Models.User;
using FreshFarm.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreshFarm.UI.Controllers
{
    public class UserController : Controller
    {
        #region Services 
        private readonly IUserUiService _userUiService;

        #endregion

        #region Constructor
        public UserController(IUserUiService userUiService)
        {
            _userUiService = userUiService;
        }
        #endregion

        #region Method

        // GET ALL
        [HttpGet]
        [Route("User/GetAll")]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                var userViewModels = await _userUiService.GetAllUsersAsync();

                if (userViewModels is null || !userViewModels.Any())
                {
                    ViewBag.ErrorMessage = "Aucun utilisateur trouvé.";
                    return View(userViewModels);
                }

                // Affichez un message de succès si un utilisateur a été ajouté ou modifié
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
                return View(userViewModels);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Une erreur est survenue : {ex.Message}";
                return View("Error", ex.Message);
            }
        }

        // GET BY ID
        [HttpGet]
        [Route("User/Details/{id}")]
        public async Task<ActionResult> GetUserById(Guid id)
        {
            try
            {
                var userDto = await _userUiService.GetUserByIdAsync(id);
                if (userDto is null)
                {
                    ViewBag.ErrorMessage = "Utilisateur non trouvé.";
                    return NotFound();
                }

                var userViewModel = new UserViewModel
                {
                    Id = userDto.Id,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Email = userDto.Email
                };

                return View(userViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Une erreur est survenue : {ex.Message}";
                return View("Error", ex.Message);
            }
        }

        // EDIT
        [HttpGet]
        [Route("User/Edit/{id}")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var userDto = await _userUiService.GetUserByIdAsync(id);

            if (userDto is null)
            {
                ViewBag.ErrorMessage = "Utilisateur non trouvé.";
                return NotFound();
            }

            var userUpdateModel = new UserUpdateModel
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email
            };

            return View(userUpdateModel);
        }

        // UPDATE
        [HttpPost]
        [Route("User/Update")]
        public async Task<ActionResult> UpdateUser(Guid id, UserUpdateDto userUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Veuillez corriger les erreurs dans le formulaire.";
                return View(userUpdateModel);
            }

            try
            {
                await _userUiService.UpdateUserAsync(id, userUpdateModel);
                TempData["SuccessMessage"] = "L'utilisateur a été mis à jour avec succès.";
                return RedirectToAction("GetAllUsers");
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.ErrorMessage = "L'utilisateur à mettre à jour n'a pas été trouvé.";
                return View(userUpdateModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Une erreur est survenue : {ex.Message}");
                ViewBag.ErrorMessage = "Une erreur s'est produite lors de la mise à jour de l'utilisateur.";
                return View(userUpdateModel);
            }
        }

        // DELETE
        [HttpPost]
        [Route("User/Delete/{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            try
            {
                var userDto = await _userUiService.GetUserByIdAsync(id);

                if (userDto is null)
                {
                    ViewBag.ErrorMessage = "Utilisateur non trouvé.";
                    return NotFound();
                }

                await _userUiService.DeleteUserAsync(id);
                TempData["SuccessMessage"] = "L'utilisateur a été supprimé avec succès.";
                return RedirectToAction("GetAllUsers");
            }
            catch (NotFoundException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.ErrorMessage = "L'utilisateur à supprimer n'a pas été trouvé.";
                return RedirectToAction("GetAllUsers");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Une erreur est survenue : {ex.Message}");
                ViewBag.ErrorMessage = "Une erreur s'est produite lors de la suppression de l'utilisateur.";
                return RedirectToAction("GetAllUsers");
            }
        }
        #endregion
    }
}



// using FreshFarm.Application.Error;
// using FreshFarm.Domain.Dtos.User;
// using FreshFarm.Domain.Service.User;
// using FreshFarm.UI.Models.User;
// using FreshFarm.UI.Services; // Importez le service UI
// using Microsoft.AspNetCore.Mvc;

// namespace FreshFarm.UI.Controllers
// {


//     public class UserController : Controller
//     {
//         #region Services 
//         private readonly IUserUiService _userUiService;

//         #endregion

//         #region Constructor
//         public UserController(IUserUiService userUiService)
//         {
//             _userUiService = userUiService;
//         }
//         #endregion

//         #region Method


//         // GET ALL

//         [HttpGet]
//         [Route("User/GetAll")]
//         public async Task<ActionResult> GetAllUsers()
//         {
//             try
//             {
//                 var userViewModels = await _userUiService.GetAllUsersAsync();

//                 if (userViewModels is null || !userViewModels.Any())
//                     return NotFound();

//                 return View(userViewModels);
//             }
//             catch (Exception ex)
//             {
//                 return View("Error", ex.Message);
//             }
//         }
//         // GET BY ID

//         [HttpGet]
//         [Route("User/Details/{id}")]
//         public async Task<ActionResult> GetUserById(Guid id)
//         {
//             try
//             {
//                 var userDto = await _userUiService.GetUserByIdAsync(id);
//                 if (userDto is null)
//                     return NotFound();

//                 var userViewModel = new UserViewModel
//                 {
//                     Id = userDto.Id,
//                     FirstName = userDto.FirstName,
//                     LastName = userDto.LastName,
//                     Email = userDto.Email
//                 };

//                 return View(userViewModel);
//             }
//             catch (Exception ex)
//             {
//                 return View("Error", ex.Message);
//             }
//         }

//         // EDIT

//         [HttpGet]
//         [Route("User/Edit/{id}")]
//         public async Task<ActionResult> Edit(Guid id)
//         {
//             var userDto = await _userUiService.GetUserByIdAsync(id);

//             if (userDto is null)
//                 return NotFound();

//             var userUpdateModel = new UserUpdateModel
//             {
//                 Id = userDto.Id,
//                 FirstName = userDto.FirstName,
//                 LastName = userDto.LastName,
//                 Email = userDto.Email
//             };

//             return View(userUpdateModel);
//         }

//         // UPDATE

//         [HttpPost]
//         [Route("User/Update")]
//         public async Task<ActionResult> UpdateUser(Guid id, UserUpdateDto userUpdateModel)
//         {
//             if (!ModelState.IsValid)
//                 return View(userUpdateModel);

//             try
//             {
//                 await _userUiService.UpdateUserAsync(id, userUpdateModel);

//                 TempData["SuccessMessage"] = "L'utilisateur a été mis à jour avec succès.";
//                 return RedirectToAction("GetAllUsers");
//             }
//             catch (NotFoundException ex)
//             {
//                 ModelState.AddModelError(string.Empty, ex.Message);
//                 return View(userUpdateModel);
//             }
//             catch (Exception ex)
//             {
//                 ModelState.AddModelError(string.Empty, $"Une erreur est survenue : {ex.Message}");
//                 return View(userUpdateModel);
//             }
//         }

//         // DELETE

//         [HttpPost]
//         [Route("User/Delete/{id}")]
//         public async Task<ActionResult> DeleteUser(Guid id)
//         {
//             try
//             {
//                 var userDto = await _userUiService.GetUserByIdAsync(id);

//                 if (userDto is null)
//                     return NotFound();

//                 await _userUiService.DeleteUserAsync(id);

//                 TempData["SuccessMessage"] = "L'utilisateur a été supprimé avec succès.";
//                 return RedirectToAction("GetAllUsers");
//             }
//             catch (NotFoundException ex)
//             {
//                 ModelState.AddModelError(string.Empty, ex.Message);
//                 return RedirectToAction("GetAllUsers");
//             }
//             catch (Exception ex)
//             {
//                 ModelState.AddModelError(string.Empty, $"Une erreur est survenue : {ex.Message}");
//                 return RedirectToAction("GetAllUsers");
//             }
//         }
//         #endregion
//     }
// }
