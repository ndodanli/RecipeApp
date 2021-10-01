using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using RecipeApp.Dtos;
using RecipeApp.Dtos.ErrorDtos;
using RecipeApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RecipeApp
{
    /**
     * A policy to check authorization and determine whether the user have permission
     * to reach a spesific method.
     */
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public PermissionHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            AccountDto account = (AccountDto)httpContextAccessor.HttpContext.Items["Account"];
            try
            {
                if (account == null)
                {
                    context.Fail();
                    throw new Exception("You are not authorized");
                }
                else
                {
                    RouteEndpoint endpoint = context.Resource as RouteEndpoint;
                    ControllerActionDescriptor descriptor = endpoint?.Metadata?
                        .SingleOrDefault(md => md is ControllerActionDescriptor) as ControllerActionDescriptor;
                    if (descriptor == null)
                        throw new InvalidOperationException("Unable to retrieve current action descriptor.");

                    RoleModel role = requirement.getAccountService()?.GetRole(account.RoleId);
                    if (role != null && role.Permissions.Contains($"{descriptor.ControllerName}.{descriptor.ActionName}", StringComparer.OrdinalIgnoreCase))
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                        throw new Exception("You are not authorized");
                    }
                }
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                if (!httpContextAccessor.HttpContext.Response.HasStarted)
                {
                    httpContextAccessor.HttpContext.Response.StatusCode = 401;
                    httpContextAccessor.HttpContext.Response.ContentType = "application/json";

                    httpContextAccessor.HttpContext.Response.WriteAsync(e.Message);
                }
                return Task.CompletedTask;
            }
        }
    }
}
