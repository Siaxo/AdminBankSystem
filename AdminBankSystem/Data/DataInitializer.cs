﻿using AdminBankSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminBankSystem.Data
{
    public class DataInitializer
    {
        private readonly BankContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public DataInitializer(BankContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public void SeedData()
        {
            _dbContext.Database.Migrate();
            SeedRoles();
            SeedUsers();
        }

        private void SeedUsers()
        {
            AddUserIfNotExists("stefan.holmberg@systementor.se", "Hejsan123#", new string[] { "Admin" });
            AddUserIfNotExists("stefan.holmberg@customer.systementor.se", "Hejsan123#", new string[] { "Cashier" });
        }

        private void SeedRoles()
        {
            AddRoleIfNotExisting("Admin");
            AddRoleIfNotExisting("Cashier");
        }

        private void AddRoleIfNotExisting(string roleName)
        {
            var role = _dbContext.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                _dbContext.Roles.Add(new IdentityRole { Name = roleName, NormalizedName = roleName });
                _dbContext.SaveChanges();
            }
        }


        private void AddUserIfNotExists(string userName, string password, string[] roles)
        {
            if (_userManager.FindByEmailAsync(userName).Result != null) return;

            var user = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };
            _userManager.CreateAsync(user, password).Wait();
            _userManager.AddToRolesAsync(user, roles).Wait();
        }


    }
}
