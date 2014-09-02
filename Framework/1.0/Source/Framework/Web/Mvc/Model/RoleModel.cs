using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Cdts.Core;

namespace Cdts.Framework.Web.Mvc
{
    public class RoleModel : FrameworkModel
    {
        public RoleModel()
        {
        }
        public RoleModel(IRole role)
        {
            if (role == null)
            {
                return;
            }
            Id = role.Id;
            Name = role.Name;
            Code = role.Code;
            Description = role.Description;
            Invalid = role.Invalid;

            if (role.Parent == null)
            {
                Parent = null;
            }
            else
            {
                Parent = new RoleModel(role.Parent);
            }
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public RoleModel Parent { get; set; }
        public string Description { get; set; }
        public bool Invalid { get; set; }
        public string Path { get; set; }
        public SelectList ParentList { get; set; }
        public IRole Fill(IGetObject getObject, IRole role)
        {
            role.Id = Id;
            role.Code = Code;
            role.Name = Name;
            role.Description = Description;
            role.Invalid = Invalid;

            if (Parent != null)
            {
                role.Parent = (IRole)getObject.GetObject("Id", Parent.Id, typeof(IRole));
            }
            else
            {
                role.Parent = null;
            }
            return role;
        }
    }
}
