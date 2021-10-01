using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeApp.Constants
{
    public class SeedPermissions
    {
        public static readonly string[] defaultAdminPermissions = { "admin.addrole", "admin.addrecipe", "admin.index", "admin.getroles", "admin.updaterole", "admin.deleterole", "admin.roles", "admin.recipes", "admin.getrecipes", "admin.getusers", "admin.adduser", "admin.updateuser", "admin.deleteuser", "admin.deleterecipe", "admin.updaterecipe", "admin.categories", "admin.addcategory", "admin.updatecategory", "admin.deletecategory", "admin.getcategories" };
        public static readonly string[] defaultModeratorPermissions = { "admin.addrecipe", "admin.index", "admin.recipes", "admin.getrecipes", "admin.getusers", "admin.deleterecipe", "admin.updaterecipe", "admin.categories", "admin.addcategory", "admin.updatecategory", "admin.deletecategory", "admin.getcategories" };
    }
}
