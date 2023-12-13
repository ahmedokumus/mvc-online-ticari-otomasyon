using System.Linq;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Models;

public class UserRoleProvider : RoleProvider
{
    public override bool IsUserInRole(string username, string roleName)
    {
        throw new System.NotImplementedException();
    }

    public override string[] GetRolesForUser(string email)
    {
        using (Context _context = new Context())
        {
            var user = _context.Employees.FirstOrDefault(x => x.Email == email);
            return new[]
            {
                user?.Role
            };
        }
    }

    public override void CreateRole(string roleName)
    {
        throw new System.NotImplementedException();
    }

    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    {
        throw new System.NotImplementedException();
    }

    public override bool RoleExists(string roleName)
    {
        throw new System.NotImplementedException();
    }

    public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    {
        throw new System.NotImplementedException();
    }

    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    {
        throw new System.NotImplementedException();
    }

    public override string[] GetUsersInRole(string roleName)
    {
        throw new System.NotImplementedException();
    }

    public override string[] GetAllRoles()
    {
        throw new System.NotImplementedException();
    }

    public override string[] FindUsersInRole(string roleName, string usernameToMatch)
    {
        throw new System.NotImplementedException();
    }

    public override string ApplicationName { get; set; }
}