using ERP.Presentation.Api.App_Start;
using ERP.Presentation.Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RH.Infra.Data.DBContexts;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RH.UI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [AllowAnonymous]
        public ActionResult Logout(string returnUrl)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult LoginTecnun()
        {

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Ajusta o dominio do login
            model.Email = model.Email.Replace("@tecnun.com.br", "") + "@tecnun.com.br";

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Registrar()
        {
            return View();
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Registrar(RegistrarContaViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //Define um novo usuário
                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    
                    //Cria a entrada do usuário no banco de dados
                    var result = await UserManager.CreateAsync(user, model.Senha);

                    //Caso o usuário tenha sido criado com sucesso, efetua o Login
                    if (result.Succeeded)
                    {
                        //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        var callbackUrl = await GerarEmailDeConfirmacao(user);

                        ViewBag.Link = callbackUrl;

                        return View("DisplayEmail");
                        //return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                }
                // If we got this far, something failed, redisplay form
                return View(model);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private async Task<string> GerarEmailDeConfirmacao(ApplicationUser user)
        {
            try
            {
                //Gera o codigo criptografado que será usado para validar o email
                var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                //Gera a URL de callback que será enviado ao email do usuário para confirmação
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                //Invoca o envio do email para a classe de serviço pre-configurada
                await UserManager.SendEmailAsync(user.Id,
                    "Confirme sua Conta",
                    "Clique no link a seguir para confirmar o cadastro :\n <br /><br /><a class='btn btn-default' href='" + callbackUrl + "'>Confirmar</a>"
                );

                //Retorna a url que foi enviada.
                return callbackUrl;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
