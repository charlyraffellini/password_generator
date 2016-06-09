using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using PasswordGenerator.Models;

namespace PasswordGenerator.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public Models.PasswordGenerator PasswordGenerator { get; set; }

        public AccountController(Models.PasswordGenerator passwordGenerator)
        {
            PasswordGenerator = passwordGenerator;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var password = this.PasswordGenerator.GetOne(new Email(model.Email));
            //bool result = FormsAuthentication.Authenticate(model.Email, password.EncryptedPassword);
            FormsAuthentication.SetAuthCookie(model.Email, false);
            return RedirectToAction("Index", "Home");
        }


        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult GeneratePassword(string code)
        {
            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> GeneratePassword(GeneratePasswordViewModel model)
        {
            var password = this.PasswordGenerator.GenerateNew(new Email(model.Email));

            return RedirectToAction("Login", "Account", new { LastPasswordGenerated = password });
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}