#region Using

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BERlogic.CallCenter.Configuration;
using BERlogic.CallCenter.Models;
using Microsoft.AspNetCore.Identity;

#endregion

namespace BERlogic.CallCenter.Data
{
    /// <summary>
    /// Helper class that ensures that the data store used by the application contains the demo user.
    /// </summary>
    public class ApplicationDbSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private bool _seeded;

        public ApplicationDbSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // We take a dependency on the manager as we want to create a valid user
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Performs the data store seeding of the demo user if it does not exist yet.
        /// </summary>
        /// <returns>A <c>bool</c> indicating whether the seeding has occurred.</returns>
        public async Task EnsureSeed()
        {
            if (!_seeded)
            {
                try
                {
                    // First we check if an existing user can be found for the configured demo credentials
                    var existingUser = await _userManager.FindByEmailAsync(SmartSettings.DemoUsername);
                    var existingAdmin = await _userManager.FindByEmailAsync("test@test.de");

                    // If an existing user was found
                    if (existingUser != null)
                    {
                        // Notify the developer
                        Console.WriteLine("Database already seeded!");

                        // Then seeding has already taken place
                        _seeded = true;
                        return;
                    }

                    if (existingAdmin != null)
                    {
                        Console.WriteLine("Admin is already exist in Database!");
                        _seeded = true;
                        return;
                    }

                    // Prepare the new user with the configured demo credentials
                    var user = new ApplicationUser
                    {
                        UserName = SmartSettings.DemoUsername,
                        Email = SmartSettings.DemoUsername
                    };
                    var admin = new ApplicationUser
                    {
                        UserName = "Admin",
                        Email = "test@test.de"
                    };
                    string adminPassword = "test!";

                    //create admin role
                    bool isAdminRoleExist = await _roleManager.RoleExistsAsync("Admin");
                    if (!isAdminRoleExist)
                    {
                        var role = new IdentityRole { Name = "Admin" };
                        await _roleManager.CreateAsync(role);
                    }
                    // creating Creating Manager role     
                    bool isRoleExist = await _roleManager.RoleExistsAsync("Manager");
                    if (!isRoleExist)
                    {
                        var role = new IdentityRole { Name = "Manager" };
                        await _roleManager.CreateAsync(role);

                    }
                    // creating Creating Employee role     
                    isRoleExist = await _roleManager.RoleExistsAsync("Operator");
                    if (!isRoleExist)
                    {
                        var role = new IdentityRole { Name = "Operator" };
                        await _roleManager.CreateAsync(role);
                    }
                    // creating Creating Test role     
                    isRoleExist = await _roleManager.RoleExistsAsync("Test");
                    if (!isRoleExist)
                    {
                        var role = new IdentityRole { Name = "Test" };
                        await _roleManager.CreateAsync(role);
                    }

                    // Attempt to create the demo user in the data store using the configured demo password
                    var result = await _userManager.CreateAsync(user, SmartSettings.DemoPassword);
                    var adminResult = await _userManager.CreateAsync(admin, adminPassword);
                    if (adminResult.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRolesAsync(admin, new List<string> { "Admin", "Manager", "Operator" });
                    }

                    if (result.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, "Test");
                    }
                    // Notify the developer whether the demo user was created successfully
                    Console.WriteLine(result.Succeeded ? "Database successfully seeded!" : "Database already seeded!");

                    // We either already have the demo user or it was just added, either way we're good
                    _seeded = true;
                    return;
                }
                catch (Exception ex)
                {
                    // Notify the developer that storing the demo user encountered an error
                    Console.Error.WriteLine("Error trying to seed the database");
                    Console.Error.WriteLine(ex);
                    return;
                }
            }

            // Notify the developer
            Console.WriteLine("Database already seeded!");
        }
    }
}
