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
        public async Task<IActionResult> Index()
        {
            if(!validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "User", action = "Index", message = "Zona restringida, inicia sesion primero", messageType="alert-danger" });
            }
            ViewBag.AddFriend = new SaveFriendViewModel();
            ViewBag.Friends = await friendService.GetAllWithIncludes();
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

            if (!ModelState.IsValid)
            {
                return View("_FormularioFriend",vm);
            }


    

            var response = await friendService.CreateAsync(vm);

            if(response == null)
            {

                ModelState.AddModelError("user exist", "Ese usuario no existe");
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
