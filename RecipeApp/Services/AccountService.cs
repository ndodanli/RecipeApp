using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using RecipeApp.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RecipeApp.Helpers;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApp.Dtos.AccountDtos;
using AutoMapper.Configuration;
using RecipeApp.IMapper;
using AutoMapper;
using IConfigE = Microsoft.Extensions.Configuration;
using System.Linq;
using RecipeApp.Utility;
using RecipeApp.Dtos;

namespace RecipeApp.Services
{
    /**
     * Account service to manage user operations.
     */
    public class AccountService : IAccountService
    {
        private readonly IMongoCollection<AccountModel> users;
        private readonly IMongoCollection<RoleModel> roles;
        private readonly AutoMapper.IMapper mapper;
        private readonly string secretKey;
        public AccountService(IConfigE.IConfiguration configuration)
        {
            //Mapper configuration.
            MapperConfigurationExpression configurationExpression = new MapperConfigurationExpression();
            configurationExpression.AddProfile(new MappingProfile());
            MapperConfiguration config = new MapperConfiguration(configurationExpression);
            mapper = config.CreateMapper();

            //Database configuration.
            MongoClient client = new MongoClient(configuration.GetConnectionString("RecipeDb"));
            IMongoDatabase database = client.GetDatabase("RecipeDb");
            users = database.GetCollection<AccountModel>("Users");
            roles = database.GetCollection<RoleModel>("Roles");
            secretKey = configuration.GetSection(nameof(AuthSettings)).GetSection("SecretKey").Value;
        }

        /**
         * Authenticate user.
         */
        public (ActionResult, string) Authenticate(AccountDto account)
        {
            AccountModel user = users.Find(x => x.Username == account.Username).FirstOrDefault();

            if (user == null || !BC.Verify(account.Password, user.Password)) return (new BadRequestObjectResult("You have entered an invalid username or password"), null);

            return (new OkResult(), GenerateJwtToken(user));

        }

        /**
         * Generate a jwt token with user's id.
         */
        private string GenerateJwtToken(IAccount account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", account.Id),
                }),
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> GetRoleIdByNameAsync(string roleName)
        {
            RoleModel role = await roles.Find(x => x.Name == roleName)
            .FirstOrDefaultAsync();

            return role.Id;
        }

        #region User Operations

        /**
         * Get all users with their role names.
         */
        public async Task<AccountRoleListDto> GetUsersAsync()
        {
            List<AccountModel> userList = await users.Find(_ => true)
                .Project<AccountModel>(Builders<AccountModel>.Projection.Exclude(x => x.Password))
                .ToListAsync();

            List<AccountListDto> userListDto = new List<AccountListDto>();

            userList.ForEach(x => userListDto.Add(mapper.Map<AccountListDto>(x)));

            foreach (AccountListDto account in userListDto)
            {
                account.Role = GetRole(account.RoleId).Name;
            }

            List<RoleListDto> roles = await GetRolesAsync();

            List<string> roleNames = new List<string>();

            foreach (RoleListDto role in roles)
            {
                roleNames.Add(role.Name);
            }
            AccountRoleListDto result = new AccountRoleListDto() { Users = userListDto, Roles = roleNames };

            return result;
        }

        /**
         * Delete a user with id.
         */
        public async Task<ActionResult> DeleteUserAsync(string id)
        {
            await users.DeleteOneAsync(x => x.Id == id);

            return new OkResult();
        }

        /**
         * Update a user with id.
         */
        public async Task<ActionResult> UpdateUserAsync(UpdateUserDto userDto)
        {
            AccountModel user = await users.Find(x => x.Id == userDto.Id)
            .FirstOrDefaultAsync();

            AccountModel isUsernameExist = await users.Find(x => x.Username == userDto.Username)
            .FirstOrDefaultAsync();

            if (isUsernameExist != null) return new BadRequestObjectResult("Username already exist");

            AccountModel userModel = mapper.Map<AccountModel>(userDto);

            //UpdateProps method checks if the sended data transfer object(userDto) properties 
            //has any null value, if so, it won't map the value to the user object.
            if (user != null)
            {
                UtilityMethods.UpdateProps<AccountModel>(user, userModel);
            }

            if (userDto.Password != null)
                user.Password = BC.HashPassword(userDto.Password);

            if (userDto.RoleName != null)
            {
                user.RoleId = await GetRoleIdByNameAsync(userDto.RoleName);
            }

            FilterDefinition<AccountModel> filter = Builders<AccountModel>.Filter.Eq(x => x.Id, userDto.Id);
            UpdateDefinition<AccountModel> update = Builders<AccountModel>.Update
            .Set(x => x.Username, user.Username)
            .Set(x => x.Password, user.Password)
            .Set(x => x.RoleId, user.RoleId);

            UpdateOptions options = new UpdateOptions { IsUpsert = true };

            await users.UpdateOneAsync(filter, update, options);

            return new OkResult();
        }

        /**
         * Add a user.
         */
        public async Task<ActionResult> AddUserAsync(AddUserDto userDto)
        {
            AccountModel user = mapper.Map<AccountModel>(userDto);

            user.Password = BC.HashPassword(user.Password);

            user.RoleId = await GetRoleIdByNameAsync(userDto.RoleName);

            AccountModel dbUser = await users.Find(x => x.Username == userDto.Username).FirstOrDefaultAsync();

            if (dbUser == null)
            {
                await users.InsertOneAsync(user);
                return new OkResult();
            }
            else
            {
                return new ConflictObjectResult("User already exist");
            }
        }
        #endregion

        #region Role Operations

        /**
         * Get roles.
         */
        public async Task<List<RoleListDto>> GetRolesAsync()
        {
            List<RoleModel> roleList = await roles.Find(_ => true)
            .ToListAsync();

            List<RoleListDto> roleListDto = new List<RoleListDto>();

            roleList.ForEach(x => roleListDto.Add(mapper.Map<RoleListDto>(x)));

            return roleListDto;
        }

        /**
         * Deleta a role with id.
         */
        public async Task<ActionResult> DeleteRoleAsync(string id)
        {
            await roles.DeleteOneAsync(x => x.Id == id);

            return new OkResult();
        }

        /**
         * Update a user with id.
         */
        public async Task<ActionResult> UpdateRoleAsync(UpdateRoleDto roleDto)
        {
            RoleModel role = await roles.Find(x => x.Id == roleDto.RoleId)
            .FirstOrDefaultAsync();

            string[] newPermissions = role.Permissions.Union(roleDto.Permissions).ToArray();

            FilterDefinition<RoleModel> filter = Builders<RoleModel>.Filter.Eq(x => x.Id, roleDto.RoleId);
            UpdateDefinition<RoleModel> update = Builders<RoleModel>.Update.Set(x => x.Permissions, newPermissions);

            UpdateOptions options = new UpdateOptions { IsUpsert = true };

            await roles.UpdateOneAsync(filter, update, options);

            return new OkResult();
        }

        /**
         * Add a role
         */
        public async Task<ActionResult> AddRoleAsync(AddRoleDto roleDto)
        {
            RoleModel role = mapper.Map<RoleModel>(roleDto);

            await roles.InsertOneAsync(role);

            return new OkResult();
        }

        /**
         * Get role with id.
         */
        public RoleModel GetRole(string roleId)
        {
            RoleModel role = roles.Find(x => x.Id == roleId).FirstOrDefault();

            if (role == null) return null;

            return role;
        }

        #endregion
    }
}
