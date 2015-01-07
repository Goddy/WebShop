using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebShop.Models
{
    public class ApplicationRoleStore: RoleStore<ApplicationRole>, IRoleStore<ApplicationRole>
    {
        public ApplicationRoleStore(DbContext context): base(context)
        {
        }
    }
}