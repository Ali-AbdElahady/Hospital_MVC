using Hospital.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hospital.DAL.Context
{
    public static class HospitalContextSeed
    {
        public static async Task SeedAsync(HospitalDbContext dbContext)
        {
            if (!dbContext.Hospitals.Any())
            {
                var HospitalsData = File.ReadAllText("../Hospital.DAL/DataSeed/Hospitals.json");
                var Hospitals = JsonSerializer.Deserialize<List<HospitalEntity>>(HospitalsData);
                if (Hospitals?.Count > 0)
                {
                    foreach (var Hospital in Hospitals)
                    {
                        await dbContext.Set<HospitalEntity>().AddAsync(Hospital);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Departments.Any())
            {
                var DepatmentsData = File.ReadAllText("../Hospital.DAL/DataSeed/Departments.json");
                var Depatments = JsonSerializer.Deserialize<List<Department>>(DepatmentsData);
                if (Depatments?.Count > 0)
                {
                    foreach (var Depatment in Depatments)
                    {
                        await dbContext.Set<Department>().AddAsync(Depatment);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Doctors.Any())
            {
                var DoctorsData = File.ReadAllText("../Hospital.DAL/DataSeed/Doctors.json");
                var Doctors = JsonSerializer.Deserialize<List<Doctor>>(DoctorsData);
                if (Doctors?.Count > 0)
                {
                    foreach (var Doctor in Doctors)
                    {
                        await dbContext.Set<Doctor>().AddAsync(Doctor);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Staffs.Any())
            {
                var StaffsData = File.ReadAllText("../Hospital.DAL/DataSeed/Staffs.json");
                var Staffs = JsonSerializer.Deserialize<List<Staff>>(StaffsData);
                if (Staffs?.Count > 0)
                {
                    foreach (var Staff in Staffs)
                    {
                        await dbContext.Set<Staff>().AddAsync(Staff);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Rooms.Any())
            {
                var RoomsData = File.ReadAllText("../Hospital.DAL/DataSeed/Rooms.json");
                var Rooms = JsonSerializer.Deserialize<List<Room>>(RoomsData);
                if (Rooms?.Count > 0)
                {
                    foreach (var Room in Rooms)
                    {
                        await dbContext.Set<Room>().AddAsync(Room);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
