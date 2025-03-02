using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Dtos.Email;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModel.User;

namespace SocialNetwork.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        private readonly IEmailService emailService;

        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly ValidateUserSession validateUserSession;

        public UserController(IUserService userService, IEmailService emailService, IHttpContextAccessor httpContextAccessor, ValidateUserSession validateUserSession)
        {
            this.userService = userService;
            this.emailService = emailService;
            this.httpContextAccessor = httpContextAccessor;
            this.validateUserSession = validateUserSession;
        }
        public IActionResult Index(string? message = null, string? messageType = null)
        {
            if (validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index", message = "Zona restringida, inicia sesion primero", messageType = "alert-danger" });
            }
            ViewBag.Message = message;
            ViewBag.MessageType = messageType;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await userService.Login(vm);

            if(user == null)
            {
                ModelState.AddModelError("User validate", "Usuario o contrasena incorrectos");
            }
            else if(!user.IsActive)
            {
                ModelState.AddModelError("Account validate", "Debe activar su cuenta a travez del link que se le envio a su correo");
            }
            else
            {
                httpContextAccessor.HttpContext.Session.Set<UserViewModel>("user", user);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            return View(vm);
        }

        public IActionResult Restore()
        {
            if (validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index", message = "Zona restringida, inicia sesion primero", messageType = "alert-danger" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Restore(ResetPasswordViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }


            var response = await userService.ResetPassword(model.Username);

            if(response != null)
            {
                await emailService.SendAsync(new EmailRequest
                {
                    To = response.Email,
                    Subject = "Su nueva contraseña!!!",
                    Body = $"<h1>Nueva contraseña de su red social</h1> <p>CONTRASEÑA: {response.Password}</p>"
                });
                return RedirectToRoute(new { controller = "User", action = "Index", message = "Se le envio su nueva contraseña a su correo", messageType = "alert-success" });
            }
            else
            {
                ModelState.AddModelError("user", "Ese nombre de usuario no existe");
            }


            return View(model);
        }

        public IActionResult Register()
        {
            if (validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index", message = "Zona restringida, inicia sesion primero", messageType = "alert-danger" });
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel userVm)
        {
            if(!ModelState.IsValid)
            {
                return View(userVm);
            }

            try
            {
                userVm.TokenActive = Guid.NewGuid().ToString();
                var response  = await userService.CreateAsync(userVm);
                response.ImagePath = UploadFile(userVm.Image, response.Id);
                await userService.UpdateAsync(response,response.Id);
                string urlActivacion = $"https://localhost:7152/User/ActivarCuenta?token={userVm.TokenActive}";
                await emailService.SendAsync(new EmailRequest
                {
                    To= response.Email,
                    Subject="Activa tu cuenta!",
                    Body= $"<p>Haz clic en el siguiente enlace para activar tu cuenta:</p> <a href='{urlActivacion}'>Activar Cuenta</a>"
                });
                return RedirectToAction("Index");
            }
            catch (DbUpdateException dbEx)
            {

                if (dbEx.InnerException?.Message.Contains("IX_Users_Email") == true)
                {
                    ModelState.AddModelError("Email", "El correo ya está registrado. Por favor, use otro.");
                }
                else if (dbEx.InnerException?.Message.Contains("IX_Users_Username") == true)
                {
                    ModelState.AddModelError("Username", "El nombre de usuario ya está en uso. Elija otro.");
                }
            }
           

            return View(userVm);
        }

        public IActionResult LogOut()
        {
            httpContextAccessor.HttpContext.Session.Remove("user");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ActivarCuenta(string token)
        {
            if (validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Home", action = "Index", message = "Zona restringida, inicia sesion primero", messageType = "alert-danger" });
            }
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index");
            }

            var usuario = await userService.PutUserActive(token);

            if(usuario != null)
            {
                return RedirectToRoute(new { controller = "User", action = "Index", message = "Usuario activado", messageType = "alert-success" });
            }
            else
            {
                return RedirectToRoute(new { controller = "User", action = "Index", message = "Algo salio mal", messageType = "alert-danger" });
            }
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
