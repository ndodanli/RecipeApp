using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using RecipeApp.Dtos;
using RecipeApp.Helpers;
using RecipeApp.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    /**
     * A middleware to handle Jwt authentication.
     * Checking if the user sent a token in cookie if so,
     * decoding the jwt token and taking it's claims.
     * Searching the user by the taken id claims and
     * attaching to context for controllers to use.
     */
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AuthSettings appSettings;
        private readonly IMongoCollection<AccountModel> users;

        public JwtMiddleware(RequestDelegate next, IOptions<AuthSettings> appSettings, IConfiguration configuration)
        {
            _next = next;
            this.appSettings = appSettings.Value;
            MongoClient client = new MongoClient(configuration.GetConnectionString("RecipeDb"));
            IMongoDatabase database = client.GetDatabase("RecipeDb");
            users = database.GetCollection<AccountModel>("Users");
        }

        public async Task Invoke(HttpContext context)
        {

            string token = context.Request.Cookies["at"];

            if (token != null)
                await attachAccountToContext(context, token);

            if (!context.Response.HasStarted)
                await _next(context);
        }

        private async Task attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                //Token validation parameters and pulling secret key from appsettings.json.
                byte[] key = Encoding.ASCII.GetBytes(appSettings.SecretKey);
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken = null;
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                //Token validation process.
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;

                //Find the user and attach it to the context.
                string userId = jwtToken.Claims.First(x => x.Type == "id").Value.ToString();

                AccountModel user = await users.Find(x => x.Id == userId).FirstOrDefaultAsync();
                if (user != null)
                {
                    context.Items["Account"] = new AccountDto()
                    {
                        RoleId = user.RoleId
                    };
                }
            }
            catch (SecurityTokenException)
            {
                //If the token is not valid and response hasn't started yet, pass the control to the next middleware.
                if (!context.Response.HasStarted)
                    await _next(context);
            }
            catch (Exception)
            {
                //If any other error happens.
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.CompleteAsync();
            }
        }
    }
}