using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace InventoryManager.MobileAppService.Models
{
    public class UserRepository : IUserRepository
    {
        private SqlDbContext dbContext;
        private readonly AppSettings appSettings;

        private static ConcurrentDictionary<string, User> users =
            new ConcurrentDictionary<string, User>();
        public UserRepository(SqlDbContext _db, IOptions<AppSettings> _appSettings)
        {
            appSettings = _appSettings.Value;
            dbContext = _db;
        }

        public void Add(User user)
        {

            //  user.Id = Guid.NewGuid().ToString();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

        }


        public User Get(int id) => dbContext.Users.Find(id);


        public IEnumerable<User> GetAll() => dbContext.Users;


        public User Remove(int userId)
        {
            User user = dbContext.Users.Find(userId);

            dbContext.Users.Remove(user);
            dbContext.SaveChanges();

            return user;
        }

        public void Update(User user)
        {
            dbContext.Users.Update(user);
            dbContext.SaveChanges();
            // if (modelstate.isValid())
        }
        public User Authenticate(string username, string password, string storeName)
        {
            var user = dbContext.Users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password.ToLower() == password.ToLower()).FirstOrDefault();
            Store store = dbContext.Stores.Where(s => s.StoreName == storeName).FirstOrDefault();
            if (user.UserType != UserType.Admin.ToString() && user.UserType !=  UserType.DistrictManager.ToString()) {
                if (user.StoreId != store.StoreId) {
                    return null;
                }
            }

            // return null if user not found
            if (user == null )
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.UserType)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

    }
}
