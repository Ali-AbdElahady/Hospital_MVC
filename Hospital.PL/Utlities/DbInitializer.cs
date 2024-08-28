using Hospital.DAL.Context;
using Hospital.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Text.Json;

namespace Hospital.PL.Utilities
{
	public class DbInitializer : IDbInitializer
	{
		private UserManager<ApplicationUser> _userManager { get; set; }
		private RoleManager<IdentityRole> _roleManager { get; set; }
		private HospitalDbContext _dbContext { get; set; }
		public DbInitializer(UserManager<ApplicationUser> userManager, 
			RoleManager<IdentityRole> roleManager, 
			HospitalDbContext dbContext)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_dbContext = dbContext;
		}

        
        public async Task Initialize()
		{
			if (await _roleManager.RoleExistsAsync(WebSiteRoles.WebSite_Admin))
			{
			 //	await _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Admin));
				//await _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Doctor));
				//await _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Staff));
    //            await _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Patient));

				await _userManager.CreateAsync(new ApplicationUser()
				{
                    FName = "Ali",
                    LName = "Ahmed",
					UserName = "Dc_ALi",
					Email = "Dc_ALi@gmail.com"
				}, "Ali@123");
                //var AdminUser = _dbContext.ApplicationUsers.FirstOrDefault(x => x.Email == "Dc_ALi@gmail.com");
                var AdminUser = await _userManager.FindByEmailAsync("Dc_ALi@gmail.com");

                if (AdminUser != null)
				{
					await _userManager.AddToRoleAsync(AdminUser, WebSiteRoles.WebSite_Admin);
				}
                if (!_userManager.Users.Any())
                {
                    var DoctorsData = File.ReadAllText("../Hospital.DAL/DataSeed/Doctors.json");
                    var Doctors = JsonSerializer.Deserialize<List<ApplicationUser>>(DoctorsData);
                    
                    if (Doctors?.Count > 0)
                    {
                        foreach (var Doctor in Doctors)
                        {
                            var User = new ApplicationUser()
                            {
                                FName = Doctor.FName,
                                LName = Doctor.LName,
                                Email = Doctor.Email,
                                UserName = Doctor.UserName,
                                PhoneNumber = Doctor.PhoneNumber,
                                Department_ID = Doctor.Department_ID,
                                Specialization_ID = Doctor.Specialization_ID,
                            };
                            await _userManager.CreateAsync(User, "Pa$$w0rdDoctor");
                            var DoctorData = _dbContext.ApplicationUsers
                                .FirstOrDefault(x => x.PhoneNumber == Doctor.PhoneNumber && x.FName == Doctor.FName);
                            await _userManager.AddToRoleAsync(DoctorData, WebSiteRoles.WebSite_Doctor);
                        }
                    }

                    var StaffsData = File.ReadAllText("../Hospital.DAL/DataSeed/Doctors.json");
                    var Staffs = JsonSerializer.Deserialize<List<ApplicationUser>>(StaffsData);

                    if (Staffs?.Count > 0)
                    {
                        foreach (var Staff in Staffs)
                        {
                            var User = new ApplicationUser()
                            {
                                FName = Staff.FName,
                                LName = Staff.LName,
                                Email = Staff.Email,
                                UserName = Staff.UserName,
                                PhoneNumber = Staff.PhoneNumber,
                                Department_ID = Staff.Department_ID,
                            };
                            await _userManager.CreateAsync(User, "Pa$$w0rdStaff");
                            var DoctorData = _dbContext.ApplicationUsers
                                .FirstOrDefault(x => x.PhoneNumber == Staff.PhoneNumber && x.FName == Staff.FName);
                            await _userManager.AddToRoleAsync(DoctorData, WebSiteRoles.WebSite_Staff);
                        }
                    }
                }
            }


		}
	}
}
