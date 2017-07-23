using FamilyReunion.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FamilyReunion.Controllers
{
    [Authorize]
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
