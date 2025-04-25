using Microsoft.AspNetCore.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/Page{code}")]
        public IActionResult PageError(int code)
        {
            if (code == 404)
            {
                return View("Page404");
            }
            else if (code == 403)
            {
                return View("Page403");
            }
            else if (code == 500)
            {
                return View("Page500");
            }
            return View("PageError");

        }
    }
}
