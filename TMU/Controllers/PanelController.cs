using Core.TMU.Convertor;
using Core.TMU.FileSite;
using Core.TMU.Genarator;
using Core.TMU.Img;
using Core.TMU.Security;
using Core.TMU.Sender;
using Core.TMU.Service.ITMUService;
using Core.TMU.Service.TMUService;
using Data.TMU.Context;

using Data.TMU.Model;
using Data.TMU.Model.Gallery;
using Data.TMU.Model.Image;
using Data.TMU.Model.Menu;
using Data.TMU.Model.Nav;
using Data.TMU.Model.News;
using Data.TMU.Model.Page;
using Data.TMU.Model.Slider;
using System;
using Data.TMU.Permissions;
using Data.TMU.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Data.TMU.Model.Course;
using Data.TMU.Model.Wallet;

namespace TMU.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class PanelController : Controller
    {
        private IUser _User;
        private IPermision _permision;
        private IViewRenderService _renderService;
        private INews _news;
        private IGallery _gallery;
        private IPage _Page;

        private IHttpContextAccessor _accessor;
        private IGeneric<News> _gNews;
        private ICourse _Course;
        private IGeneric<Course> _gCourse;
        private IGeneric<FileCourse> _gFileCourse;
        private IGeneric<Gallery> _gGallery;
        private IGeneric<FileNews> _gFileNews;
        private IGeneric<FilePage> _gFilePage;
        private IGeneric<FileGallery> _gFileGallery;
        private IGeneric<Menu> _gMenu;
        private IGeneric<Page> _gPage;
        private IGeneric<ImagePage> _gImagePage;
        private IGeneric<Slider> _gSlider;
        private IGeneric<Navbar> _gNavbar;
        private IGeneric<Role> _grole;
        private Imenu _Menu;
        private ISlider _Slider;
        private INavbar _Navbar;
        public PanelController(IUser User, IGeneric<Role> grole, IGeneric<FileCourse> gFileCourse, ICourse Course, IGeneric<Course> gCourse, IPermision permision, IViewRenderService renderService, INews news, IGeneric<FilePage> gFilePage, IGeneric<News> gNews, IGeneric<FileNews> gFileNews, IGeneric<Menu> gMenu, Imenu Menu, IGeneric<Slider> gSlider, ISlider Slider, IGeneric<Page> gPage, IPage Page, INavbar Navbar, IGeneric<Navbar> gNavbar, IGeneric<ImagePage> gImagePage, IGeneric<Gallery> gGallery, IGeneric<FileGallery> gFileGallery, IGallery gallery)
        {
            _news = news;
            _User = User;
            _gCourse = gCourse;
            _gFileCourse = gFileCourse;
            _grole = grole;
            _gSlider = gSlider;
            _permision = permision;
            _Course = Course;
            _renderService = renderService;
            _gFileNews = gFileNews;
            _gFilePage = gFilePage;
            _gImagePage = gImagePage;
            _gNews = gNews;
            _gMenu = gMenu;
            _Menu = Menu;
            _Slider = Slider;
            _gPage = gPage;
            _Page = Page;
            _Navbar = Navbar;
            _gNavbar = gNavbar;
            _gGallery = gGallery;
            _gFileGallery = gFileGallery;
            _gallery = gallery;


        }
        public IActionResult Index()
        {
          ViewBag.AllUser = _User.GetAllUser(1, null, null, null, null, 6);
            return View();
        }

        #region rejisteruser whit admin
        [permissionChecker(4)]
        [Route("panel/registerUser")]
        public IActionResult registerUser()
        {
            return View();
        }
        [permissionChecker(4)]
        [Route("panel/registerUser")]
        [HttpPost]
        public IActionResult registerUser(registerUser user)
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
                post = "0"

            };
            _User.AddUser(AddUser);
            //Unit.Save();
            string Body = _renderService.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعال سازی پنل دانشکده منابع طبیعی و علوم دریایی تربیت مدرس ", Body);
            return Redirect("ListUser");

        }


        [permissionChecker(4)]
        [Route("panel/ListUser")]
        public IActionResult ListUser(List<string> checkdelete, string IdActive = "none", int pageid = 1, string filterfullname = null, string email = null, string filteridcode = null, string tag = null)
        {
            if (IdActive != "none")
            {
                _User.ActiveUserIdCode(IdActive);
            }
            if (checkdelete.Count() != 0)
            {
                _User.DeleteListUser(checkdelete);
            }
            ViewBag.filterfullname = filterfullname;
            ViewBag.tag = filteridcode;
            //ViewBag.tag = tag;
            ViewBag.AllUser = _User.GetAllUser(1, null, null, null, null, 20);
            return View(_User.GetAllUser(pageid, filterfullname, email, filteridcode, tag, 200));
        }

        [permissionChecker(4)]
        [Route("panel/DeleteListUser/{codeid}")]
        public IActionResult DeleteListUser(List<string> checkdelete, string codeid)
        {
            if (checkdelete.Count != 0)
            {
                _User.DeleteListUser(checkdelete);
            }
            else
            {
                _User.DeleteUser(codeid);
            }
            return Redirect("/Panel/listuser");
        }

        [permissionChecker(4)]
        [Route("panel/Resetpassworduser")]
        public IActionResult Resetpassworduser()
        {
            return View();
        }

        [permissionChecker(4)]
        [Route("panel/Resetpassworduser")]
        [HttpPost]
        public IActionResult Resetpassworduser(ChangePasswordUserViewModel model, string idcode)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _User.FindUser(idcode);
            if (!_User.ComparePassword(model.NewPassword, model.ConfarinPassword))
            {
                ModelState.AddModelError("Password", "کلمه عبور با تکرار کلمه عبور همخوانی ندارد");
                return View(user);
            }
            user.Password = model.NewPassword.EncodePasswordMd5();
            _User.UpdateUser(user);
            return Redirect("/Listuser");
        }
        #endregion

        #region profile


        [Route("panel/profilesettings")]
        public IActionResult profilesettings(string idcode)
        {
            ViewBag.Role = new SelectList(_permision.GetRole(), "Value", "Text");
            var user = _User.FindUserForUpdate(User.Identity.Name);
            if (idcode != null)
            {
                user = _User.FindUserForUpdate(idcode);
                ViewBag.isEnabelpost = true;

            }
            else
            {
                ViewBag.isEnabelpost = false;
            }
            return View(user);
        }

        [HttpPost]
        [Route("panel/profilesettings")]
        public IActionResult profilesettings(UpdateregisterViewModel updateregister, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Role = new SelectList(_permision.GetRole(), "Value", "Text");
                return View(updateregister);
            }
            var user = _User.FindUser(updateregister.IdCode);
            user.CodePerseneli= (_User.FindUser(updateregister.IdCode)).ToString();
            user.Email = updateregister.Email;
            user.FullName = updateregister.FullName;
            user.Mobile = updateregister.Mobile;
            user.post = updateregister.post;
            if (updateregister.post != "0")
            {
                user.level = _permision.GetRoleById(int.Parse(updateregister.post)).Level;//پست همان رول ای دی است
                List<int> listrole = new List<int>()
                {
                    int.Parse(updateregister.post)
                };
                AddUserRoles(user.IdCode, listrole);
            }
            else
            {
                user.level = "0";
            }
            user.CodePerseneli = updateregister.CodePerseneli;
            if (user.CodePerseneli != updateregister.CodePerseneli || updateregister.CodePerseneli == null)
            {
                if (_User.FindUserwhitcodeperseneli(updateregister.CodePerseneli) || updateregister.CodePerseneli == null)
                {
                    ModelState.AddModelError("CodePerseneli", "در وارد کردن کد پرسنلی دقت نمایید");
                    ViewBag.Role = new SelectList(_permision.GetRole(), "Value", "Text");
                    return View(updateregister);
                }
            }

            //user.level = updateregister.level;
            if (file != null)
            {
                if (file.Isimage())
                {
                    user.UserAvatar = SaveImage.SaveImageProfile(file, user.UserAvatar, "wwwroot/Img/Userprofile").Item2;
                }
                else
                {
                    ModelState.AddModelError("UserAvatar", "فایل را درست انتخاب کنید");
                }
            }
            _User.UpdateUser(user);
            //Logout();
            return Redirect("/panel/listuser");

        }
        [Route("panel/ResetPassword")]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("panel/ResetPassword")]
        public IActionResult ResetPassword(ChangePasswordViewModel changePassword)
        {

            if (!ModelState.IsValid)
            {
                return View(changePassword);
            }
            var user = _User.FindUser(User.Identity.Name);

            if (changePassword.OldPassword.EncodePasswordMd5() != user.Password)
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور قدیمی را درست وارد کنید");
                return View(changePassword);
            }

            if (!_User.ComparePassword(changePassword.NewPassword, changePassword.ConfarinPassword))
            {
                ModelState.AddModelError("Password", "کلمه عبور با تکرار کلمه عبور همخوانی ندارد");
                return View(user);
            }
            user.Password = changePassword.NewPassword.EncodePasswordMd5();
            _User.UpdateUser(user);
            Logout();
            ViewBag.IsResetpassword = true;
            return Redirect("/Login?IsResetpassword=true");

        }
        #endregion

        #region permition role
        [permissionChecker(4)]
        [Route("panel/GetInfopermitionRole")]
        public IActionResult GetInfopermitionRole(int radiostackedRoleid)
        {
            ViewData["Role"] = _permision.GetAllRole();
            ViewData["Permision"] = _permision.GetAllPermission();
            //ViewData["Permisionselect"] = _permision.GetpermissionWhitRoleId(radiostackedRoleid);

            return View();
        }

        [permissionChecker(4)]
        [Route("panel/permision")]
        public IActionResult permision()
        {
            Getinfopagepermistion();
            return View();
        }

        [permissionChecker(4)]
        [Route("panel/permision")]
        [HttpPost]
        public IActionResult permision(List<int> SelectListItem, string NameRole, Role role, int radiostackedRoleid, bool DeleteRole, bool final, bool PPrint, bool EndP, bool Signuchure)
        {

            if (radiostackedRoleid != 0)
            {

                Getinfopagepermistion();
                ViewData["Permisionselect"] = _permision.GetpermissionWhitRoleId(radiostackedRoleid);
                ViewBag.roleid = radiostackedRoleid;
                ViewBag.RoleSelect = _permision.GetRoleById(radiostackedRoleid);
                ViewBag.Isfinal = _permision.GetRoleById(radiostackedRoleid).Finalapproval;//تایید نهایی
                ViewBag.EndProcess = _permision.GetRoleById(radiostackedRoleid).EndProcess;//چرخه اخر
                ViewBag.Signuchure = _permision.GetRoleById(radiostackedRoleid).Signuchure;//امضا
                ViewBag.Checkout = _permision.GetRoleById(radiostackedRoleid).Checkout;//Gis
                ViewBag.IsPermissionPrint = _permision.GetRoleById(radiostackedRoleid).PermissionPrint;// سطح 1 -کارشناس

                return View(_permision.GetRoleById(radiostackedRoleid));
            }
            //کارشناس gis است
            if (SelectListItem.Contains(7))
            {
                role.Checkout = true;
            }
            else
            {
                role.Checkout = false;
            }

            if (int.Parse(role.Level) > _permision.GetMaxLevel() + 1)
            {
                ModelState.AddModelError("RoleTitle", "سطح را زیاد انتخاب کرده اید");
                Getinfopagepermistion();
                return View();
            }
            if (_permision.GetRoleWhitLevel(role.Level) == true && role.RoleId == null)
            {
                ModelState.AddModelError("RoleTitle", "چنین سطحی قبلا تعریف شده است عدد سطح را تغییر دهید");
                Getinfopagepermistion();
                return View();
            }
            else
            {
                if (EndP == true)
                {
                    role.EndProcess = true;
                }
                if (Signuchure == true)
                {
                    role.Signuchure = true;
                }
                if (final == true)
                {
                    if (_permision.IsTrueFinalaproval())
                    {

                        _permision.GetFinalRole().Finalapproval = final;

                    }
                    if (_permision.GetPermitionPrintRole() != null)
                    {
                        if (int.Parse(_permision.GetPermitionPrintRole().Level) < int.Parse(role.Level))
                        {
                            role.Finalapproval = final;
                        }
                        else
                        {
                            ModelState.AddModelError("RoleTitle", "سطح تایید کننده دستور کار باید بالاتر از تایید کننده نهایی باشد");
                            Getinfopagepermistion();
                            return View();

                        }
                    }
                    else
                    {
                        ModelState.AddModelError("RoleTitle", "تایید کننده نهایی در سیتم ابتدا باید ثبت شود");
                        Getinfopagepermistion();
                        return View();

                    }


                }
                if (PPrint == true)
                {
                    if (_permision.IsTruePermitionPrint())
                    {
                        _permision.GetPermitionPrintRole().PermissionPrint = PPrint;

                    }
                    if (_permision.GetFinalRole() != null)
                    {
                        if (int.Parse(_permision.GetFinalRole().Level) > int.Parse(role.Level))
                        {
                            role.PermissionPrint = PPrint;
                        }
                        else
                        {
                            ModelState.AddModelError("RoleTitle", "سطح تایید کننده دستور کار باید کمتر از تایید کننده نهایی باشد");
                            Getinfopagepermistion();
                            return View();
                        }
                    }
                    else
                    {
                        role.PermissionPrint = PPrint;
                    }



                }

                if (role == null)
                {
                    int RoleId = _permision.CreatRole(new Role
                    {
                        RoleTitle = NameRole,
                        IsDelete = false,

                    });


                    _permision.AddPermission(SelectListItem, RoleId);
                }
                else
                {
                    if (DeleteRole != true)
                    {
                        if (!string.IsNullOrEmpty(role.RoleTitle))
                        {
                            _permision.UpdateRoleTask(role);
                            //_grole.Update(role)
                            _permision.UpdatePermissionRole(SelectListItem, role.RoleId);
                        }
                        else
                        {
                            ModelState.AddModelError("RoleTitle", "نام نقش را درست وارد کنید.");

                        }
                    }
                    else
                    {
                        if (_User.IsExistPost(role.RoleId) != true)
                        {
                            var u = _permision.GetRoleById(role.RoleId);
                            u.IsDelete = true;
                            _permision.UpdateRoleTask(u);
                            //_permision.DeleteRole(role);
                        }

                    }


                }
                Getinfopagepermistion();
                return Redirect("/panel/permision");
            }
        }

        //[permissionChecker(1)]
        //[Route("panel/AccesslevelUser")]
        //public IActionResult AccesslevelUser(string urlidcode)
        //{
        //    ViewBag.idcode = urlidcode;
        //    ViewData["roleuser"] = _permision.GetRoleUserWhitRoleId(_User.getPkId(urlidcode));
        //    return View(_permision.GetAllRole());
        //}

        //[permissionChecker(1)]
        //[Route("panel/AccesslevelUser")]
        //[HttpPost]
        //public IActionResult AccesslevelUser(string idcode, List<int> SelectListItem)
        //{

        //    _permision.AddRole(SelectListItem, _User.getPkId(idcode));
        //    var level = "";
        //    foreach (var item in SelectListItem)
        //    {
        //        level += _permision.GetlevelRole(item) + "-";
        //    }
        //    _User.InsertLevelUser(level, idcode);

        //    return Redirect("ListUser");
        //}


        #endregion

        #region course

        
        [Route("panel/ListcoursUser")]
        public IActionResult ListcoursUser(int pageid = 1)
        {

            return View(_Course.GetAllCourseUser(pageid,7, User.Identity.Name));
        }

        [permissionChecker(1)]
        [Route("panel/Listcourse")]
        public IActionResult Listcourse(int pageid = 1, string filtertitel = null, string tag = null)
        {
            ViewBag.filtertitel = filtertitel;
            ViewBag.tag = tag;
            return View(_Course.GetAllCourse(pageid, filtertitel, tag, 7, User.Identity.Name));
        }
        [permissionChecker(1)]
        [Route("panel/creatcourse")]
        public IActionResult creatcourse()
        {
            return View();
        }

        [permissionChecker(1)]
        [Route("panel/creatcourse")]
        [HttpPost]
        public IActionResult creatcourse(Course model, IFormFile? fileimg)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if (fileimg != null)
            {
                var image = SaveImage.SaveImageNews(fileimg, "wwwroot/Img/course/Org");
                convertorimage.Image_resize("wwwroot/Img/course/Org/" + image.Item2, "wwwroot/Img/course/thum/" + image.Item2, 500);
                model.img = image.Item2;
            }
            
            _gCourse.Insert(model);
            return Redirect("/panel/Listcourse");
        }
        [permissionChecker(1)]
        [Route("panel/Updatecourse/{id?}")]
        public IActionResult Updatecourse(int id)
        {
            var t = _gCourse.GetById(id);
            return View(t);
        }

        [permissionChecker(1)]
        [Route("panel/Updatecourse/{id?}")]
        [HttpPost]
        public IActionResult Updatecourse(Course model, IFormFile? fileimg)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (fileimg != null)
            {
                var image = SaveImage.SaveImageNews(fileimg, "wwwroot/Img/course/Org");
                convertorimage.Image_resize("wwwroot/Img/course/Org/" + image.Item2, "wwwroot/Img/course/thum/" + image.Item2, 500);
                DeleteImage.Deleteimage(model.img, "wwwroot/Img/course/Org");
                DeleteImage.Deleteimage(model.img, "wwwroot/Img/course/thum");
                model.img = image.Item2;
            }

            _gCourse.Update(model);
            return Redirect("/panel/Listcourse");
        }
        [permissionChecker(1)]
        [Route("panel/Deletecourse/{id}")]
        public IActionResult Deletecourse(int id)
        {
            var model = _gCourse.GetById(id);
            if (_Course.GetCoutVideoCourse(id)>0)
            {
                return Redirect("/panel/Listcourse");
            }
            if (model!=null)
            {

                    DeleteImage.Deleteimage(model.img, "wwwroot/Img/course/Org");
                    DeleteImage.Deleteimage(model.img, "wwwroot/Img/course/thum");

                    _gCourse.Delete(model.Id);

            }
            return Redirect("/panel/Listcourse");
        }

        [permissionChecker(1)]
        [Route("panel/creatVideocourse/{id}/{name?}")]
        public IActionResult creatVideocourse(int id)
        {
             
            if (_gCourse.GetById(id)==null)
            {
                return Redirect("/panel/Listcourse");
            }
            ViewBag.course = _gCourse.GetById(id);
            return View();
        }
        [Route("panel/creatVideocourse/{id}/{name?}")]
        [HttpPost]
        public IActionResult creatVideocourse(FileCourse model, IFormFile? filevideo)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.course = _gCourse.GetById(model.IdFC);
                return View(model);
            }
            if (filevideo != null)
            {
                var file = Save.SaveFile(filevideo, "wwwroot/video/Course", null);

                model.file = file.Item2;
            }
            _gFileCourse.Insert(model);
            return Redirect("/panel/Listcourse");
        }

        [permissionChecker(1)]
        [Route("panel/ListVideocourse/{id}")]
        public IActionResult ListVideocourse(int id, int pageid = 1, string filtertitel = null)
        {

            if (_gCourse.GetById(id) == null)
            {
                return Redirect("/panel/Listcourse");
            }
            ViewBag.filtertitel = filtertitel;
            ViewBag.course = _gCourse.GetById(id);
            return View(_Course.GetAllVideoCourse(pageid, filtertitel,15,id));
        }


        [permissionChecker(1)]
        [Route("panel/UpdateVideocourse/{id}/{name?}")]
        public IActionResult UpdateVideocourse(int id)
        {

            if (_gFileCourse.GetById(id) == null)
            {
                return Redirect("/panel/Listcourse");
            }
            return View(_gFileCourse.GetById(id));
        }
        [Route("panel/UpdateVideocourse/{id}/{name?}")]
        [HttpPost]
        public IActionResult UpdateVideocourse(FileCourse model, IFormFile? filevideo)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (filevideo != null)
            {
                var file = Save.SaveFile(filevideo, "wwwroot/video/Course", null);

                model.file = file.Item2;
            }
            _gFileCourse.Update(model);
            return Redirect("/panel/ListVideocourse/" + model.IdC);
        }

        [permissionChecker(1)]
        [Route("panel/Deletevideocourse/{id}")]
        public IActionResult Deletevideocourse(int id)
        {
            var model = _gFileCourse.GetById(id);
            if (model != null)
            {

                Delete.DeleteFile(model.file, "wwwroot/video/Course");

                _gFileCourse.Delete(model.IdFC);

            }
            return Redirect("/panel/ListVideocourse/"+ model.IdC);
        }


        #endregion

        #region Wallet

        [Route("panel/Wallet")]
        public IActionResult Wallet()
        {
            ViewBag.wallet = _User.GetListWallet(User.Identity.Name);
            return View();
        }

        [Route("panel/ChargeWallet")]
        [HttpPost]
        public IActionResult ChargeWallet(ChargeWalletViewMode model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _User.AddWallet(model.Amount, true, User.Identity.Name);
            //todo bank
            return Redirect("/panel/Wallet");
        }


        #endregion

        #region news
        [permissionChecker(2)]
        [Route("panel/ListNews")]
        public IActionResult ListNews(List<string> checkdelete, int pageid = 1, string filtertitel = null, string tag = null)
        {

            if (checkdelete.Count() != 0)
            {
                //_news.DeleteL(checkdelete);
            }
            ViewBag.filtertitel = filtertitel;
            ViewBag.tag = tag;
            return View(_news.GetAllNews(pageid, filtertitel, tag, 10,false, User.Identity.Name));
        }

        [permissionChecker(2)]
        [Route("panel/DeleteNews/{id}")]
        public IActionResult DeleteNews(int id)
        {
            if (_news.GetImageNews(id).Count != 0)
            {
                foreach (var item in _news.GetImageNews(id))
                {
                    DeleteImage.Deleteimage(_news.FindImageNews(item.Id).PathFile, "wwwroot/Img/News/Org");
                    DeleteImage.Deleteimage(_news.FindImageNews(item.Id).PathFile, "wwwroot/Img/News/thum");

                    _gFileNews.Delete(item.Id);

                }
            }


            _gNews.Delete(id);
            return Redirect("/panel/ListNews");
        }

        [permissionChecker(2)]
        [Route("panel/DetailImageNews/{id}")]
        public IActionResult DetailImageNews(int id)
        {
            ViewBag.ID = id;

            if (_news.NewsIsImage(id))
            {
                ViewBag.IsImageNews = true;
                return View(_news.GetImageNews(id));
            }
            return Redirect("/panel/ListNews");
        }

        [permissionChecker(2)]
        [Route("panel/DetailImageNews/{id}")]
        [HttpPost]
        public IActionResult DetailImageNews(List<FileNews> fileNews, string? SelectFirst, List<int>? CheckDelete, int id)
        {
            if (!string.IsNullOrEmpty(SelectFirst))
            {
                _news.SetFirstImage(int.Parse(SelectFirst), id);
                return Redirect("/panel/DetailImageNews/{id}");
            }
            if (CheckDelete.Count() != 0)
            {
                foreach (var item in CheckDelete)
                {
                    DeleteImage.Deleteimage(_news.FindImageNews(item).PathFile, "wwwroot/Img/News/Org");
                    DeleteImage.Deleteimage(_news.FindImageNews(item).PathFile, "wwwroot/Img/News/thum");
                    _gFileNews.DeleteWhitList(CheckDelete);
                }
                if (!ModelState.IsValid == true)
                {
                    return View(fileNews);
                }
                // to do change filenews
            }
            return Redirect("/panel/ListNews");
        }

        [permissionChecker(2)]
        [Route("panel/CreatImageNews/{id}")]
        public IActionResult CreatImageNews(int id)
        {
            return View();
        }

        [permissionChecker(2)]
        [Route("panel/CreatImageNews/{id}")]
        [HttpPost]
        public IActionResult CreatImageNews(FileNewsViewModel file, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(file);
            }
            foreach (var item in file.files)
            {
                var image = SaveImage.SaveImageNews(item, "wwwroot/Img/News/Org");
                convertorimage.Image_resize("wwwroot/Img/News/Org/" + image.Item2, "wwwroot/Img/News/thum/" + image.Item2, 500);
                FileNews fileNews = new FileNews()
                {
                    IdN = id,
                    PathFile = image.Item2
                };
                _gFileNews.Insert(fileNews);
            }
            return Redirect("/panel/DetailImageNews/{id}");
        }

        [permissionChecker(2)]
        [Route("panel/CreatNews")]
        public IActionResult CreatNews()
        {
            RefreshMenu();
            return View();
        }

        [permissionChecker(2)]
        [Route("panel/CreatNews")]
        [HttpPost]
        public IActionResult CreatNews(News model, bool chk1)
        {
            RefreshMenu();
            model.CountView = 0;
            model.IsSearch = false;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _gNews.Insert(model);
            return Redirect("/panel/ListNews");
        }

        [permissionChecker(2)]
        [Route("panel/UpdateNews/{id?}")]
        public IActionResult UpdateNews(int id)
        {
            RefreshMenu();

            var News = _gNews.GetById(id);

            return View(News);
        }

        [permissionChecker(2)]
        [Route("panel/UpdateNews/{id?}")]
        [HttpPost]
        public IActionResult UpdateNews(News model, bool chk1)
        {
            RefreshMenu();
            //if (model.Forest.Class!=0)
            //{
            //    GetSelect2(model.Forest.Class);
            //}   

            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            if (!chk1)
            {
                _gNews.Update(model);
                model.IsSearch = false;

            }
            else
            {

                ModelState.AddModelError("Forest.NameForest", "اطلاعات جستجو پیشرفته را کامل کنید");
                return View(model);
            }


            return Redirect("/panel/ListNews");
        }
        #endregion



        #region Galery


        [permissionChecker(5)]
        [Route("panel/ListGallery")]
        public IActionResult ListGallery(List<string> checkdelete, int pageid = 1, string filtertitel = null, string tag = null)
        {

            if (checkdelete.Count() != 0)
            {
                //_news.DeleteL(checkdelete);
            }
            ViewBag.filtertitel = filtertitel;
            ViewBag.tag = tag;
            return View(_gallery.GetAllGallery(pageid, filtertitel, tag, 7));
        }

        [permissionChecker(5)]
        [Route("panel/DeleteGalery/{id}")]
        public IActionResult DeleteGalery(int id)
        {
            if (_gallery.GetImageGallery(id).Count != 0)
            {
                foreach (var item in _gallery.GetImageGallery(id))
                {
                    DeleteImage.Deleteimage(_gallery.FindImageGallery(item.Id).PathFile, "wwwroot/Img/Gallery/Org");
                    DeleteImage.Deleteimage(_gallery.FindImageGallery(item.Id).PathFile, "wwwroot/Img/Gallery/thum");

                    _gFileGallery.Delete(item.Id);

                }
            }

            _gGallery.Delete(id);
            return Redirect("/panel/Listgallery");
        }

        [permissionChecker(5)]
        [Route("panel/DetailImageGallery/{id}")]
        public IActionResult DetailImageGallery(int id)
        {
            ViewBag.ID = id;

            if (_gallery.GalleryIsImage(id))
            {
                ViewBag.IsImagegallery = true;
                return View(_gallery.GetImageGallery(id));
            }
            return Redirect("/panel/ListGallery");
        }

        [permissionChecker(5)]
        [Route("panel/DetailImageGallery/{id}")]
        [HttpPost]
        public IActionResult DetailImageGallery(List<FileGallery> fileGalleries, string? SelectFirst, List<int>? CheckDelete, int id)
        {
            if (!string.IsNullOrEmpty(SelectFirst))
            {
                _gallery.SetFirstImage(int.Parse(SelectFirst), id);
                return Redirect("/panel/DetailImageGallery/{id}");
            }
            if (CheckDelete.Count() != 0)
            {
                foreach (var item in CheckDelete)
                {
                    DeleteImage.Deleteimage(_gallery.FindImageGallery(item).PathFile, "wwwroot/Img/Gallery/Org");
                    DeleteImage.Deleteimage(_gallery.FindImageGallery(item).PathFile, "wwwroot/Img/Gallery/thum");
                    _gFileGallery.DeleteWhitList(CheckDelete);
                }
                if (!ModelState.IsValid == true)
                {
                    return View(fileGalleries);
                }
                // to do change filenews
            }
            return Redirect("/panel/ListGallery");
        }

        [permissionChecker(5)]
        [Route("panel/CreatImageGallery/{id}")]
        public IActionResult CreatImageGallery(int id)
        {
            return View();
        }

        [permissionChecker(5)]
        [Route("panel/CreatImageGallery/{id}")]
        [HttpPost]
        public IActionResult CreatImageGallery(FileGalleryViewModel file, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(file);
            }
            foreach (var item in file.files)
            {
                var image = SaveImage.SaveImageNews(item, "wwwroot/Img/Gallery/Org");
                convertorimage.Image_resize("wwwroot/Img/Gallery/Org/" + image.Item2, "wwwroot/Img/Gallery/thum/" + image.Item2, 500);
                FileGallery FileGallery = new FileGallery()
                {
                    IdG = id,
                    PathFile = image.Item2
                };
                _gFileGallery.Insert(FileGallery);
            }
            return Redirect("/panel/DetailImageGallery/{id}");
        }

        [permissionChecker(5)]
        [Route("panel/CreatGallery")]
        public IActionResult CreatGalery()
        {

            return View();
        }

        [permissionChecker(5)]
        [Route("panel/CreatGallery")]
        [HttpPost]
        public IActionResult CreatGalery(Gallery model, bool chk1)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.View = 0;
            _gGallery.Insert(model);

            return Redirect("/panel/ListGallery");
        }

        [permissionChecker(5)]
        [Route("panel/UpdateGallery/{id?}")]
        public IActionResult UpdateGalery(int id)
        {
            return View(_gGallery.GetById(id));
        }

        [permissionChecker(5)]
        [Route("panel/UpdateGalery/{id?}")]
        [HttpPost]
        public IActionResult UpdateGalery(Gallery model, bool chk1)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _gGallery.Update(model);
            return Redirect("/panel/ListGallery");
        }


        #endregion

        #region page
        //[permissionChecker(1003)]
        //public IActionResult ListPage(List<string> checkdelete, int pageid = 1, string filtertitel = null, string tag = null)
        //{

        //    if (checkdelete.Count() != 0)
        //    {
        //        //_news.DeleteL(checkdelete);
        //    }
        //    ViewBag.filtertitel = filtertitel;
        //    ViewBag.tag = tag;
        //    return View(_Page.GetAllPage(pageid, filtertitel, tag, 7));
        //}

        //[permissionChecker(1003)]
        //[Route("panel/DeletePage/{id}")]
        //public IActionResult DeletePage(int id)
        //{
        //    if (_Page.GetImagePage(id).Count != 0)
        //    {
        //        foreach (var item in _Page.GetImagePage(id))
        //        {
        //            DeleteImage.Deleteimage(_Page.FindImagePage(item.Id).PathFile, "wwwroot/Img/Page/Org");
        //            DeleteImage.Deleteimage(_Page.FindImagePage(item.Id).PathFile, "wwwroot/Img/Page/thum");

        //            _gFilePage.Delete(item.Id);

        //        }
        //    }


        //    _gPage.Delete(id);
        //    return Redirect("/panel/ListPage");
        //}

        //[permissionChecker(1003)]
        //[Route("Panel/CreatPage")]
        //public IActionResult CreatPage()
        //{

        //    return View();
        //}

        //[permissionChecker(1003)]
        //[Route("Panel/CreatPage")]
        //[HttpPost]
        //public IActionResult CreatPage(Page page)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(page);
        //    }
        //    _gPage.Insert(page);
        //    return Redirect("/Panel/ListPage");
        //}

        //[permissionChecker(1003)]
        //[Route("panel/UpdatePage/{id?}")]
        //public IActionResult UpdatePage(int id)
        //{
        //    return View(_gPage.GetById(id));
        //}

        //[permissionChecker(1003)]
        //[Route("panel/UpdatePage/{id?}")]
        //[HttpPost]
        //public IActionResult UpdatePage(Page model)
        //{


        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return View(model);
        //    //}
        //    _gPage.Update(model);
        //    return Redirect("/Panel/ListPage");
        //}



        //[permissionChecker(1003)]
        //[Route("panel/DetailImagePage/{id}")]
        //public IActionResult DetailImagePage(int id)
        //{
        //    ViewBag.ID = id;

        //    if (_Page.PageIsImage(id))
        //    {
        //        ViewBag.IsImagePage = true;
        //        return View(_Page.GetImagePage(id));
        //    }
        //    return Redirect("/panel/ListPage");
        //}

        //[permissionChecker(1003)]
        //[Route("panel/DetailImagePage/{id}")]
        //[HttpPost]
        //public IActionResult DetailImagePage(List<FilePage> filePage, string? SelectFirst, List<int>? CheckDelete, int id)
        //{
        //    if (!string.IsNullOrEmpty(SelectFirst))
        //    {
        //        _Page.SetFirstImage(int.Parse(SelectFirst), id);
        //        return Redirect("/panel/DetailImagePage/{id}");
        //    }
        //    if (CheckDelete.Count() != 0)
        //    {
        //        foreach (var item in CheckDelete)
        //        {
        //            DeleteImage.Deleteimage(_Page.FindImagePage(item).PathFile, "wwwroot/Img/Page/Org");
        //            DeleteImage.Deleteimage(_Page.FindImagePage(item).PathFile, "wwwroot/Img/Page/thum");
        //            _gFilePage.DeleteWhitList(CheckDelete);
        //        }
        //        if (!ModelState.IsValid == true)
        //        {
        //            return View(filePage);
        //        }
        //        // to do change filenews
        //    }
        //    return Redirect("/panel/ListPage");
        //}

        //[permissionChecker(1003)]
        //[Route("panel/CreatImagePage/{id}")]
        //public IActionResult CreatImagePage(int id)
        //{
        //    return View();
        //}

        //[permissionChecker(1003)]
        //[Route("panel/CreatImagePage/{id}")]
        //[HttpPost]
        //public IActionResult CreatImagePage(FilePageViewModel file, int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(file);
        //    }
        //    foreach (var item in file.files)
        //    {
        //        var image = SaveImage.SaveImageNews(item, "wwwroot/Img/Page/Org");
        //        convertorimage.Image_resize("wwwroot/Img/Page/Org/" + image.Item2, "wwwroot/Img/Page/thum/" + image.Item2, 500);
        //        FilePage filePage = new FilePage()
        //        {
        //            IdP = id,
        //            PathFile = image.Item2
        //        };
        //        _gFilePage.Insert(filePage);
        //    }
        //    return Redirect("/panel/DetailImagePage/{id}");
        //}


        //[permissionChecker(1003)]
        //[Route("panel/InsertImagePage")]
        //public IActionResult InsertImagePage()
        //{
        //    return View();
        //}
        //[permissionChecker(1003)]
        //[Route("panel/InsertImagePage")]
        //[HttpPost]
        //public IActionResult InsertImagePage(ImagePageViewModel file)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return View(file);
        //    //}
        //    foreach (var item in file.files)
        //    {
        //        var image = SaveImage.SaveImageNews(item, "wwwroot/Img/ImageSite/Org");
        //        convertorimage.Image_resize("wwwroot/Img/ImageSite/Org/" + image.Item2, "wwwroot/Img/ImageSite/thum/" + image.Item2, 500);
        //        file.ImagePage.PathFile = image.Item2;
        //        _gImagePage.Insert(file.ImagePage);
        //    }
        //    return Redirect("/panel/ShowImagePage");
        //}


        //[permissionChecker(1003)]
        //[Route("panel/ShowImagePage")]
        //public IActionResult ShowImagePage(int pageid = 1, string filtertitel = null, string tag = null)
        //{
        //    return View(_Page.GetAllImagePage(pageid, filtertitel, tag, 12));
        //}
        //[permissionChecker(1003)]
        //[Route("panel/DeleteImagePage")]
        //[HttpPost]
        //public IActionResult DeleteImagePage(List<FilePage> filePage, List<int>? CheckDelete)
        //{
        //    //if (!string.IsNullOrEmpty(SelectFirst))
        //    //{
        //    //    _Page.SetFirstImage(int.Parse(SelectFirst), id);
        //    //    return Redirect("/panel/DetailImagePage/{id}");
        //    //}
        //    if (CheckDelete.Count() != 0)
        //    {
        //        foreach (var item in CheckDelete)
        //        {
        //            DeleteImage.Deleteimage(_Page.FindImageSite(item).PathFile, "wwwroot/Img/ImageSite/Org");
        //            DeleteImage.Deleteimage(_Page.FindImageSite(item).PathFile, "wwwroot/Img/ImageSite/thum");
        //            _gImagePage.DeleteWhitList(CheckDelete);
        //        }
        //        //if (!ModelState.IsValid == true)
        //        //{
        //        //    return View(filePage);
        //        //}
        //        // to do change filenews
        //    }
        //    return Redirect("/panel/ShowImagePage");
        //}

        #endregion

        //#region navbar

        //[permissionChecker(3)]
        //[Route("panel/ListNavbar/{filtertitel?}")]
        //public IActionResult ListNavbar(List<string> checkdelete, int pageid = 1, string filtertitel = null)
        //{

        //    if (checkdelete.Count() != 0)
        //    {
        //        //_news.DeleteL(checkdelete);
        //    }
        //    ViewBag.filtertitel = filtertitel;
        //    return View(_Navbar.GetAllNavbar(pageid, filtertitel, 20, "خبری"));
        //}

        //[permissionChecker(3)]
        //[Route("/panel/CreatNavbar")]
        //public IActionResult CreatNav()
        //{
        //    RefreshNavbar();
        //    return View();
        //}

        //[permissionChecker(3)]
        //[Route("/panel/CreatNavbar")]
        //[HttpPost]
        //public IActionResult CreatNav(Navbar nav, bool chk1, bool chk2, string? ParentId2)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(nav);
        //    }
        //    if (chk1==false)
        //    {
        //        nav.ParentId = 0;
        //    }
        //    if (chk1 && chk2)
        //    {
        //        nav.ParentId = int.Parse(ParentId2);
        //    }

        //    _gNavbar.Insert(nav);
        //    return Redirect("/Panel/ListNavbar");
        //}
        //[permissionChecker(3)]
        //[Route("/panel/UpdateNavbar/{id?}")]
        //public IActionResult UpdateNavbar(int id)
        //{
        //    RefreshNavbar();
        //    if (id != 0)
        //    {
        //        return View(_gNavbar.GetById(id));
        //    }
        //    else
        //    {
        //        return Redirect("/Panel/ListNavbar");
        //    }
        //}


        //[permissionChecker(3)]
        //[Route("/panel/UpdateNavbar/{id?}")]
        //[HttpPost]
        //public IActionResult UpdateNavbar(Navbar nav, bool chk1)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(nav);
        //    }
        //    if (chk1 == false && !_Navbar.IsParent(nav.Id))
        //    {
        //        nav.ParentId = 0;
        //    }
        //    _gNavbar.Update(nav);
        //    return Redirect("/Panel/ListNavbar");
        //}

        //[permissionChecker(3)]
        //[Route("/panel/DeleteNavbar/{id?}")]
        //public IActionResult DeleteNavbar(int id)
        //{
        //    if (id != 0 && !_Navbar.IsParent(id))
        //    {
        //        _gNavbar.Delete(id);

        //    }
        //    return Redirect("/Panel/ListNavbar");
        //}

        //#endregion

        #region menu
        [permissionChecker(3)]
        [Route("panel/Menu")]
        public IActionResult AddMenu()
        {
            RefreshMenu();
            return View();
        }
        [permissionChecker(3)]
        [Route("panel/Menu")]
        [HttpPost]
        public IActionResult AddMenu(Menu menu, string? ParentId2, bool chk1, bool chk2)
        {
            RefreshMenu();
            if (!ModelState.IsValid)
            {
                return View(menu);
            }
            if (chk1 == false)
            {
                menu.ParentId = 0;
            }
            if (!string.IsNullOrEmpty(ParentId2) && chk2 == true && chk1 == true)
            {
                menu.ParentId = int.Parse(ParentId2);
            }
            _gMenu.Insert(menu);

            return Redirect("/panel/ListMenu");
        }

        [permissionChecker(3)]
        [Route("panel/UpdateMenu/{id}")]
        public IActionResult UpdateMenu(int id)
        {
            RefreshMenu();
            ViewBag.id = id;
            var f = _gMenu.GetById(id);
            ViewBag.infoMenu = f;
            return View(f);
        }

        [permissionChecker(3)]
        [Route("panel/UpdateMenu/{id}")]
        [HttpPost]
        public IActionResult UpdateMenu(Menu menu, string? ParentId2, bool chk1, bool chk2)
        {
            RefreshMenu();
            if (!ModelState.IsValid)
            {
                return View(menu);
            }
            //if (chk1 == false)
            //{
            //    menu.ParentId = 0;
            //}
            //if (!string.IsNullOrEmpty(ParentId2) && chk2 == true && chk1 == true)
            //{
            //    menu.ParentId = int.Parse(ParentId2);
            //}
            _gMenu.Update(menu);

            return Redirect("/panel/ListMenu");
        }

        [permissionChecker(3)]
        [Route("panel/ListMenu/{filtertitel?}")]
        public IActionResult ListMenu(List<string> checkdelete, int pageid = 1, string filtertitel = null)
        {

            if (checkdelete.Count() != 0)
            {
                //_news.DeleteL(checkdelete);
            }
            ViewBag.filtertitel = filtertitel;
            return View(_Menu.GetAllMenu(pageid, filtertitel, 7));
        }

        [permissionChecker(3)]
        [Route("panel/DeleteMenu/{id}")]
        public IActionResult DeleteMenu(int id)
        {
            if (_gMenu.GetById(id) != null)
            {
                _gMenu.Delete(id);
            }
            return View("/Panel/ListMenu");
        }
        #endregion

        #region slider

        //[permissionChecker(2)]
        //[Route("panel/ListSlider")]
        //public IActionResult ListSlider(List<string> checkdelete, int pageid = 1, string filtertitel = null)
        //{

        //    if (checkdelete.Count() != 0)
        //    {
        //        //_news.DeleteL(checkdelete);
        //    }
        //    ViewBag.filtertitel = filtertitel;

        //    return View(_Slider.GetAllSlider(pageid, filtertitel, 7));
        //}

        //[permissionChecker(2)]
        //[Route("panel/CreatSlider")]
        //public IActionResult CreatSlider()
        //{

        //    return View();
        //}

        //[permissionChecker(2)]
        //[Route("panel/CreatSlider")]
        //[HttpPost]
        //public IActionResult CreatSlider(SliderViewModel model)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    model.slider.DetaSlider = DateTime.Now;
        //    var image = SaveImage.SaveImageNews(model.fileimage, "wwwroot/Img/Slider/Org");
        //    convertorimage.Image_resize("wwwroot/Img/Slider/Org/" + image.Item2, "wwwroot/Img/Slider/thum/" + image.Item2, 500);
        //    model.slider.FileName = image.Item2;
        //    _gSlider.Insert(model.slider);


        //    return Redirect("/panel/ListSlider");
        //}

        //[permissionChecker(2)]
        //[Route("panel/UpdateSlider/{id?}")]
        //public IActionResult UpdateSlider(int id)
        //{
        //    SliderViewModel model = new SliderViewModel()
        //    {
        //        fileimage = null,
        //        slider = _gSlider.GetEntity(p => p.Id == id)
        //    };
        //    return View(model);
        //}

        //[permissionChecker(2)]
        //[Route("panel/UpdateSlider/{id?}")]
        //[HttpPost]
        //public IActionResult UpdateSlider(SliderViewModel model)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    model.slider.DetaSlider = DateTime.Now;
        //    if (model.fileimage != null)
        //    {
        //        DeleteImage.Deleteimage(model.slider.FileName, "wwwroot/Img/Slider/Org");
        //        DeleteImage.Deleteimage(model.slider.FileName, "wwwroot/Img/Slider/thum");
        //        var image = SaveImage.SaveImageNews(model.fileimage, "wwwroot/Img/Slider/Org");
        //        convertorimage.Image_resize("wwwroot/Img/Slider/Org/" + image.Item2, "wwwroot/Img/Slider/thum/" + image.Item2, 500);
        //        model.slider.FileName = image.Item2;

        //    }
        //    _gSlider.Update(model.slider);
        //    return Redirect("/panel/ListSlider");
        //}

        //[permissionChecker(2)]
        //[Route("panel/DeleteSlider/{id}")]
        //public IActionResult DeleteSlider(int id)
        //{
        //    var slider = _gSlider.GetById(id);
        //    DeleteImage.Deleteimage(slider.FileName, "wwwroot/Img/Slider/Org");
        //    DeleteImage.Deleteimage(slider.FileName, "wwwroot/Img/Slider/thum");
        //    _gSlider.Delete(id);
        //    return Redirect("/panel/ListSlider");
        //}
        #endregion






        public void Getinfopagepermistion()
        {
            ViewData["Role"] = _permision.GetAllRole();
            ViewData["Permision"] = _permision.GetAllPermission();


        }
        public void Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        }
        

        public void RefreshMenu()
        {
            ViewBag.select1 = new SelectList(_Menu.GetSelect1(), "Value", "Text");
        }
        public void RefreshNavbar()
        {
            ViewBag.select1courseNavbar = new SelectList(_Navbar.GetSelect1("آموزشی"), "Value", "Text");
            ViewBag.select1Navbar = new SelectList(_Navbar.GetSelect1(), "Value", "Text");
            try
            {
                ViewBag.select2Navbar = new SelectList(_Navbar.GetSelect2(int.Parse(_Navbar.GetSelect1().First().Value)), "Value", "Text");
            }
            catch (Exception)
            {

            }
        }
        public ActionResult GetNavbar2(int id)
        {
            var subgroup = _Navbar.GetSelect2(id);
            if (subgroup.Count() != 0)
            {
                return Json(new SelectList(subgroup, "Value", "Text"));
            }
            else
            {
                return null;
            }

        }
        public ActionResult GetSelect2(int id)
        {
            var subgroup = _Menu.GetSelect2(id);
            if (subgroup.Count() != 0)
            {
                return Json(new SelectList(subgroup, "Value", "Text"));
            }
            else
            {
                return null;
            }

        }

        [Route("panel/UploadFile")]
        public ActionResult UploadFile(IFormFile? file)
        {
            if (file != null)
            {
                if (IsExist.ExistFile("gharardad.pdf", @"wwwroot\file\") == true)
                {
                    Delete.DeleteFile("gharardad.pdf", @"wwwroot\file\");
                }
                Save.SaveFile(file, @"wwwroot\file\", "gharardad.pdf");
                return Redirect("/panel");
            }
            return Redirect("ProfileHome");

        }
        [Route("panel/UploadFileFactor")]
        public ActionResult UploadFileFactor(IFormFile? file, string namefile)
        {
            if (file != null)
            {
                if (IsExist.ExistFile(namefile + ".pdf", @"wwwroot\file\Factor\") == true)
                {
                    Delete.DeleteFile(namefile + ".pdf", @"wwwroot\file\Factor\");
                }
                Save.SaveFile(file, @"wwwroot\file\Factor\", namefile + ".pdf");
                return Redirect("/Statusface");
            }
            return Redirect("Statusface");

        }


        //ثبت نقش به کاربر
        public IActionResult AddUserRoles(string idcode, List<int> SelectListItem)
        {

            _permision.AddRole(SelectListItem, _User.getPkId(idcode));
            return Redirect("ListUser");
        }

    }
}
