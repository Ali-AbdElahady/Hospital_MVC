using Hospital.DAL.Entites;
using Microsoft.AspNetCore.Identity;
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
            if (!dbContext.Specializations.Any())
            {
                var SpecializationsData = File.ReadAllText("../Hospital.DAL/DataSeed/Specializations.json");
                var Specializations = JsonSerializer.Deserialize<List<Specialization>>(SpecializationsData);
                if (Specializations?.Count > 0)
                {
                    foreach (var Specialization in Specializations)
                    {
                        await dbContext.Set<Specialization>().AddAsync(Specialization);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
