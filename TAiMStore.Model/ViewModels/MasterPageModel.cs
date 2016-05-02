using System.Collections.Generic;
using System.Web.Mvc;

namespace TAiMStore.Model.ViewModels
{
    public class MasterPageModel
    {
        public UserViewModel UserViewModel { get; set; }
        public ProductsViewModel ProductsViewModel { get; set; }
        public ProductViewModel ProductView { get; set; }
    }
}
