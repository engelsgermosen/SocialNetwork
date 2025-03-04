using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModel.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Controllers
{
    public class PerfilController : Controller
    {
        private readonly IUserService userService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserViewModel userViewModel;
        private readonly ValidateUserSession validateUserSession;

        public PerfilController(IUserService userService, IHttpContextAccessor httpContextAccessor, ValidateUserSession validateUserSession)
        {
            this.userService = userService;
            this.httpContextAccessor = httpContextAccessor;
            userViewModel = httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            this.validateUserSession = validateUserSession;
        }
        public async Task<IActionResult> Index(string? message, string? messageType)
        {
            if (!validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index", message = "Zona restringida, inicia sesion primero", messageType = "alert-danger" });
            }
            ViewBag.Message = message;  
            ViewBag.MessageType = messageType;
            return View(await userService.GetByIdSaveViewModel(userViewModel.Id));
        }
        [HttpPost]
        public async Task<IActionResult> Index(SaveUserViewModel vm)
        {
            if (!validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index", message = "Zona restringida, inicia sesion primero", messageType = "alert-danger" });
            }
            if (string.IsNullOrWhiteSpace(vm.Password) && string.IsNullOrWhiteSpace(vm.ConfirmPassword))
            {
                ModelState.Remove(nameof(vm.Password));
                ModelState.Remove(nameof(vm.ConfirmPassword));
            }
            ModelState.Remove(nameof(vm.Image));

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var phoneValid = ValidatePhone.NumeroPermitido(vm.Phone);

            if (!phoneValid)
            {
                ModelState.AddModelError("phone formar", "El numero no tiene el formato de RD");
                return View(vm);
            }

            try
            {
                vm.ImagePath = UploadFile(vm.Image, vm.Id, true, vm.ImagePath);
                var usuarioOld = await userService.UpdateAsync(vm,vm.Id);


                return RedirectToRoute(new { controller = "Perfil", action = "Index", message = "Usuario actualizado satisfactoriamente", messageType = "alert-success" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error",ex.Message);
                
            }

            return View(vm);
        }
        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }

            string basePath = $"/Images/User/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }

            return $"{basePath}/{fileName}";
        }
    }
}
