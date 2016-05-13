using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TAiMStore.Domain;
using TAiMStore.HtmlHelpers;
using TAiMStore.Model;
using TAiMStore.Model.Abstract;
using TAiMStore.Model.Classes;
using TAiMStore.Model.Repository;
using TAiMStore.Model.UnitOfWork;
using TAiMStore.Model.ViewModels;

namespace TAiMStore.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IContactsRepository _contactsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _passwordMinLength = 6;
        private static bool IsEdit = false;

        public AccountController(IUserRepository userRepository, IRoleRepository roleRepository, IContactsRepository contactsRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _contactsRepository = contactsRepository;
            _unitOfWork = unitOfWork;
        }

        #region  Registration

        public ActionResult Register()
        {
            return View(new UserViewModel());
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(UserViewModel user, string userName, string email, string password, string confirmPassword)
        {

            if (user.Captcha != (string)Session["code"])
            {
                ModelState.AddModelError("Captcha", "Текст с картинки введен неверно");
            }
            else
            {
                var manager = new UserManager(_userRepository, _roleRepository, _contactsRepository, _unitOfWork);
                if (ValidateRegistration(userName, email, password, confirmPassword))
                {
                    // Создание пользователя
                    user = manager.RegisterUser(userName, email, password);

                    // Вход

                    return View("RegisterSuccess", user);
                }
            }           
            return View(user);
        }

        #endregion

        #region Validate

        private bool ValidateUserName(string userName)
        {
            var manager = new UserManager(_userRepository, _roleRepository, _contactsRepository, _unitOfWork);
            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("_Form", "Вы должны ввести логин.");
            }
            else
            {
                if (manager.GetUserByName(userName) != null)
                {
                    ModelState.AddModelError("_Form", "Данный логин уже зарегистрирован.");
                    return ModelState.IsValid;
                }

                if (Regex.IsMatch(manager.ToLowerUserName(userName), @"(^[^A-Za-z])|[-]{2}|([-]$)|([^A-Za-z0-9-]+)"))
                {
                    ModelState.AddModelError("_Form", "Некорректный логин.");
                    return ModelState.IsValid;
                }

                if (userName.Length > 15)
                {
                    ModelState.AddModelError("_Form", "Логин должен быть не более 15 символов.");
                    return ModelState.IsValid;
                }

                if (userName.Length < 4)
                {
                    ModelState.AddModelError("_Form", "Логин должен быть не менее 4 символов.");
                    return ModelState.IsValid;
                }
            }
            return ModelState.IsValid;
        }

        private bool ValidateRegistration(string userName, string email, string password, string confirmPassword)
        {
            var manager = new UserManager(_userRepository, _roleRepository, _contactsRepository, _unitOfWork);
            if (!ValidateUserName(userName))
            {
                ModelState.AddModelError("userName", "Некорректный логин");
            }

           if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "Введите E-Mail.");
            }
            else
            {
                if (email.Length > 30)
                {
                    ModelState.AddModelError("email", "E-Mail должен содержать не более 30 символов.");
                }
                if (!Regex.IsMatch(email, @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"))
                {
                    ModelState.AddModelError("email", "Введите корректный E-Mail.");
                }
                var user = manager.GetUserByEmail(email);
                if (user != null)
                {
                    ModelState.AddModelError("_FORM", "Такой email уже зарегистрирован.");
                    return ModelState.IsValid;
                }
            }
            if (string.IsNullOrEmpty(password) || password.Length < _passwordMinLength)
            {
                ModelState.AddModelError("password",
                    String.Format(CultureInfo.CurrentCulture,
                         "Пароль должен содержать {0} или более символов.",
                         _passwordMinLength));
            }
            if (!String.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ModelState.AddModelError("_FORM", "Новый пароль и подтверждение пароля должны совпадать.");
            }
            return ModelState.IsValid;
        }

        public bool ValidateUser(string userName, string password)
        {
            var user = _userRepository.Get(u => u.Name == userName);
            if (user == null) return false;
            else
            {
                if (user.Password != HashHelper.HashPassword(password))
                {
                    return false;
                }
                else return true;
            }
        }
        #endregion

        # region method

        public bool ChangePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            if (ValidateUser(userName, oldPassword))
            {
                var user = _userRepository.Get(u => u.Name == userName);
                if (newPassword.Equals(confirmPassword))
                {
                    user.Password = HashHelper.HashPassword(newPassword);

                    _userRepository.Update(user);
                    _unitOfWork.Commit();

                    return true;
                }
                else return false;
            }
            else return false;
        }

        private UserViewModel RegisterUser(string userName, string email, string pass)
        {
            var user = new User();
            var userViewModel = new UserViewModel();
            user.Name = userName;
            user.Email = email;
            user.Password = HashHelper.HashPassword(pass);
            user.Role = _roleRepository.Get(r => r.Name == ConstantStrings.CustomerRole);
            _userRepository.Add(user);
            _unitOfWork.Commit();

            userViewModel.Name = userName;
            userViewModel.Email = email;

            return userViewModel;
        }


        public ActionResult Captcha()
        {
            string code = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();
            Session["code"] = code;
            Captcha captcha = new Captcha(code, 110, 50);

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            captcha.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            captcha.Dispose();
            return null;
        }
        #endregion

        # region Old
        IAuthProvider authProvider;
        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин или пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
      
        }
        #endregion
    }
}