using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Enums;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModel.Comments;
using SocialNetwork.Core.Application.ViewModel.Post;
using SocialNetwork.Core.Application.ViewModel.User;

namespace SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly ValidateUserSession validateUserSession;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserViewModel userViewModel;
       
        public HomeController(IPostService postService, ICommentService commentService, ValidateUserSession validateUserSession, IHttpContextAccessor httpContextAccessor)
        {
            _postService = postService;
            _commentService = commentService;
            this.validateUserSession = validateUserSession;
            this.httpContextAccessor = httpContextAccessor;
            userViewModel = httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<IActionResult> Index()
        {
            if (!validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index", message = "Zona restringida, inicia sesion primero", messageType = "alert-danger" });
            }
            ViewBag.Guardar = new SavePostViewModel();
            ViewBag.Commentarios = new SaveCommentViewModel();
            var response = await _postService.GetAllNewPostViewModels();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePostViewModel vm)
        {
            if (!validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index", message = "Zona restringida, inicia sesion primero", messageType = "alert-danger" });
            }

            var response = await _postService.CreateAsync(vm);

            if(vm.Image != null)
            {
                response.ImagePath = UploadFile(vm.Image, response.UserId, response.Id);
                await _postService.UpdateAsync(response,response.Id);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateComments(string CommentText, int PostId,int? ParentCommentId, int Type)
        {
            if (!validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index", message = "Zona restringida, inicia sesion primero", messageType = "alert-danger" });
            }
            await _commentService.CreateAsync(new SaveCommentViewModel { Message = CommentText,PostId=PostId,ParentCommentId= ParentCommentId });

            if(Type == (int)CommentType.Post)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else 
            {
                return RedirectToRoute(new {controller="Friend",action="Index" });
            }
        }

        public async Task<IActionResult> DeletePost(int postId)
        {
            await _postService.DeleteAsync(postId);

            string basePath = $"Images/Post/{userViewModel.Id}/{postId}";
            string finalPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/{basePath}");

            if(Directory.Exists(finalPath))
            {
                Directory.Delete(finalPath, true);
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int postId)
        {

            return View(await _postService.GetByIdSaveViewModel(postId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePostViewModel post)
        {
            post.ImagePath = UploadFile(post.Image,post.UserId, post.Id, true, post.ImagePath);
            await _postService.UpdateAsync(post,post.Id);

            return RedirectToAction("Index");
        }


        private string UploadFile(IFormFile file,int userId, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }

            string basePath = $"/Images/Post/{userId}/{id}";
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
