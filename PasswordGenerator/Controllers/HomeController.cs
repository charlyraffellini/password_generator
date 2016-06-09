using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PasswordGenerator.Controllers
{
	public class HomeController : Controller
	{
        [System.Web.Mvc.AuthorizeAttribute]
        public ActionResult Index()
		{
			return View();
		}
	}
}