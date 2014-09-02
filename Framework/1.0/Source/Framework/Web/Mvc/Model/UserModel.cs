using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework.Web.Mvc
{
    public class UserModel : FrameworkModel
    {
        public UserModel()
            : this(null)
        {
        }
        public UserModel(IUser user)
        {
            Fill(user);
        }
        public void Fill(IUser user)
        {
            if (user == null)
            {
                return;
            }
            Id = user.Id;
            Name = user.Name;
            Nick = user.Nick;
            Email = user.Email;
            Code = user.Code;
            Description = user.Description;
            Invalid = user.Invalid;
            EmailValidated = user.EmailValidated;
            Mobile = user.Mobile;
            MobileValidated = user.MobileValidated;
            IsThdParty = user.IsThdParty;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }
        public string Mobile { get; set; }
        //public string Email { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Invalid { get; set; }
        public bool EmailValidated { get; set; }
        public bool MobileValidated { get; set; }
        public bool IsThdParty { get; set; }
    }
}
