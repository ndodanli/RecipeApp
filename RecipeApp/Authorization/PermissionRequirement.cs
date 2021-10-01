using Microsoft.AspNetCore.Authorization;
using RecipeApp.Services;

namespace RecipeApp
{
    /**
     * "Permission" policy requirement.
     */
    public class PermissionRequirement : IAuthorizationRequirement
    {
        private IAccountService accountService;
        public PermissionRequirement(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IAccountService getAccountService()
        {
            return accountService;
        }

    }
}