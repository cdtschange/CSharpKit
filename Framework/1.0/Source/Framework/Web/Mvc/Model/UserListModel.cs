using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework.Web.Mvc
{
    public class UserListModel : FrameworkListModel
    {
        public UserListModel()
            : base()
        {
            CurrentModel = new UserModel();
            Selector = "Id,Name,Nick,Code,Description,EmailValidated,MobileValidated,Mobile,Email";
        }
        public UserModel CurrentModel { get; set; }
    }
}
