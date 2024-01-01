using Core.TMU.Context;
using Core.TMU.Convertor;
using Core.TMU.Genarator;
using Core.TMU.Security;
using Core.TMU.Sender;
using Core.TMU.Service.ITMUService;
using Data.TMU.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using DNTCaptcha.Core;
using Data.TMU.User;

namespace TMU.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {

        //UnitOfWork Unit = new UnitOfWork();
        private IUser _User;
        private IViewRenderService _renderService;
        private IGeneric<Useronline> _gUseronline;
       
        public AccountController(IUser User, IViewRenderService renderService, IGeneric<Useronline> gUseronline)
        {
            _User = User;
            _renderService = renderService;
            _gUseronline = gUseronline;
            
        }
        #region login
        [Route("Login")]
        public IActionResult Login(string returnUrl,bool IsResetpassword=false)
        {
            ViewBag.returnUrl = returnUrl;
            ViewBag.IsResetpassword = IsResetpassword;
            return View();
        }

        [Route("Login")]
        [HttpPost]
        //[ValidateDNTCaptcha(ErrorMessage = "CAPTCHA validation failed.")]
        [ValidateDNTCaptcha(ErrorMessage = "کد امنیتی را درست وارد کنید")]
        public IActionResult Login(LoginViewModel login,string? returnUrl)
        {
         
            if (!ModelState.IsValid)
            {
                return View(login);
            }


            var user = _User.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.FullName.ToString()),
                        new Claim(ClaimTypes.Name,user.IdCode),
                        
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("/panel");
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                }
            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
        }

        #endregion

        #region register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [Route("Register")]
        [HttpPost]
        public IActionResult Register(registerUser user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (!_User.ComparePassword(user.Password, user.RePassword))
            {
                ModelState.AddModelError("Password", "کلمه عبور با تکرار کلمه عبور همخوانی ندارد");
                return View(user);
            }
            if (_User.IsExistEmail(user.Email))
            {
                ModelState.AddModelError("Email", "همچین ایمیلی قبلا ثبت نام شده است");
                return View(user);
            }
            if (_User.IsExistEmail(user.Mobile))
            {
                ModelState.AddModelError("Mobile", "همچین شماره همراهی قبلا ثبت نام شده است");
                return View(user);
            }
            User AddUser = new User()
            {
                Email = user.Email,
                FullName = user.FullName,
                Mobile = user.Mobile,
                Password = (user.Password).EncodePasswordMd5(),
                RegisterDate = DateTime.Now,
                IsActive = false,
                IdCode = user.IdCode,
                ActiveCode = Code.UniqCode(),
                UserAvatar = "DefultAvatar.jpg",
                IsNormalUser=user.IsNormalUser,
                post="0"
            };
            if (_User.GetAllUser(1, null, null, null, null, 7).Listuser.Count == 0)
            {
                AddUser.IsAdmin = true;
            }
            _User.AddUser(AddUser);
            //Unit.Save();
            string Body = _renderService.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعال سازی پنل دانشکده منابع طبیعی و علوم دریایی تربیت مدرس ", Body);
            return View("Confirm", user);
        }

        
        public IActionResult Confirm()
        {
            return View();
        }
        #endregion

        #region Logout
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
           //await _chatHub.Stopconnection(User.Identity.Name);
           await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
           return  Redirect("/Login");
        }
        #endregion 
        #region ActiveEmail

        public IActionResult ActiveEmail(String id)
        {

            ViewBag.IsActiv = _User.ActiveUser(id);
            return Redirect("Login/"+ ViewBag.IsActiv);
        }


        #endregion
    }
}
