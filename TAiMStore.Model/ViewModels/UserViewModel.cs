using System.Collections.Generic;
using System.Web.Mvc;

namespace TAiMStore.Model.ViewModels
{
    public class UserViewModel
    {
        public string Id;
        public string LoginName;
        public string Password;
        public string ConfirmPassword;
        public string Email;
        public List<SelectListItem> Roles;
    }
}
