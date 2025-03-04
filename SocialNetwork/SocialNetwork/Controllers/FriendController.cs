using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModel.Friend;

namespace SocialNetwork.Controllers
{
    public class FriendController : Controller
    {
        private readonly IFriendService friendService;
        private readonly IPostService postService;
        private readonly ValidateUserSession validateUserSession;


        public FriendController(IFriendService friendService, IPostService postService, ValidateUserSession validateUserSession)
        {
            this.friendService = friendService;
            this.postService = postService;
            this.validateUserSession = validateUserSession;
        }
        public async Task<IActionResult> Index(string? message = null, string? messageType=null)
        {
            if(!validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index", message = "Zona restringida, inicia sesion primero", messageType="alert-danger" });
            }
            ViewBag.AddFriend = new SaveFriendViewModel();
            ViewBag.Friends = await friendService.GetAllWithIncludes();
            ViewBag.Message = message;
            ViewBag.MessageType = messageType;
            return View(await postService.GetAllFriendsPostViewModel());
        }

        public async Task<IActionResult> CreateFriend(SaveFriendViewModel vm)
        {
            if (!validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index", message = "Zona restringida, inicia sesion primero", messageType = "alert-danger" });
            }
            ViewBag.AddFriend = new SaveFriendViewModel();
            ViewBag.Friends = await friendService.GetAllWithIncludes();

            var response = await friendService.CreateAsync(vm);

            if(response == null)
            {

                return RedirectToRoute(new { controller = "Friend", action = "Index", message = "No existe un usuario con ese nombre de usuario", messageType = "alert-danger" });
            }
            else if(response.UserId == -1)
            {
                return RedirectToRoute(new { controller = "Friend", action = "Index", message = "No te puedes agregar como amigo a ti mismo", messageType = "alert-danger" });
            }

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int friendId)
        {
            if (!validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index", message = "Zona restringida, inicia sesion primero", messageType = "alert-danger" });
            }
            await friendService.DeleteFriend(friendId);
            return RedirectToAction("Index");
        }
    }
}
