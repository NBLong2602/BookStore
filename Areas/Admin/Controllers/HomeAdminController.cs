using BookStore.Areas.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/Dashbroad")]
    public class HomeAdminController : Controller
    {
        [Route("")]
        [Route("index")]
        [Authentication]
        [AdminAuthorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
