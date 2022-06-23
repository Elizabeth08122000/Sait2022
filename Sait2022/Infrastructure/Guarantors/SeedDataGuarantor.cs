using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sait2022.Security;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;

namespace Sait2022.Infrastructure.Guarantors
{
    /// <summary>
    /// Заполнение данных для DbContext
    /// </summary>
    public class SeedDataGuarantor
    {
        private readonly IServiceProvider _serviceProvider;
        public SeedDataGuarantor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Проверка данных
        /// </summary>
        public async Task EnsureAsync()
        {
            var context = _serviceProvider.GetService<SaitDbContext>();
            var userManager = _serviceProvider.GetService<UserManager<Users>>();
            var roleManager = _serviceProvider.GetService<RoleManager<IdentityRole<int>>>();

            await AssertRoleExistenceAsync(roleManager, context);

            context.SaveChanges();

            var adminUser = userManager.FindByNameAsync(SecurityConstants.AdminUserName).Result;
            if (adminUser == null)
            {

                var profile = new Employee
                {
                    FirstName = SecurityConstants.AdminFirstName,
                    Surname = SecurityConstants.AdminSurName,
                    IsAdministrator = true
                };

                adminUser = new Users
                {
                    Email = SecurityConstants.AdminEmail,
                    UserName = SecurityConstants.AdminUserName,
                    Employee = profile
                };

                var passwordHasher = new PasswordHasher<Users>();

                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, SecurityConstants.AdminPassword);

                await userManager.CreateAsync(adminUser);
            }

            if (!userManager.IsInRoleAsync(adminUser, SecurityConstants.AdminRole).Result)
                userManager.AddToRoleAsync(adminUser, SecurityConstants.AdminRole).Wait();


            context.SaveChanges();
        }
        private static async Task AssertRoleExistenceAsync(RoleManager<IdentityRole<int>> roleManager, SaitDbContext context)
        {
            var roles = new List<IdentityRole<int>>
            {
                new IdentityRole<int> { Name = SecurityConstants.AdminRole, NormalizedName = "ADMIN" },
                new IdentityRole<int> { Name = SecurityConstants.TeacherRole, NormalizedName = "TEACHER" },
                new IdentityRole<int> { Name = SecurityConstants.StudentRole, NormalizedName = "STUDENT" }
            };

            foreach (var role in roles)
            {
                var roleExit = await roleManager.RoleExistsAsync(role.Name);
                if (!roleExit)
                {
                    context.Roles.Add(role);
                    context.SaveChanges();
                }
            }

        }
    }
}
