using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SeedData
    {
        public static async Task InitAsync(ApplicationDbContext context)
        {

            // step 1: get some coordinates data here
            var coordinatesSeed = new List<Coordinates>
            {
                new Coordinates(40.7128f, -74.0060f), // New York
                new Coordinates(34.0522f, -118.2437f), // Los Angeles
                new Coordinates(51.5074f, -0.1278f) // London
            };

            // step 2: seed coordinates
            await context.AddRangeAsync(coordinatesSeed);
            await context.SaveChangesAsync(); // Ensure IDs are generated before Vendors use them

            // step 3: get seeded coordinates ids
            var coordinatesList = await context.Coordinates.ToListAsync();

            // step 4: create some vendors
            var vendorsSeed = new List<Vendor>
            {
                new Vendor { Name = "Vendor NYC", CoordinatesId = coordinatesList[0].Id },
                new Vendor { Name = "Vendor LA", CoordinatesId = coordinatesList[1].Id },
                new Vendor { Name = "Vendor London", CoordinatesId = coordinatesList[2].Id }
            };

            // step 5: seed vendors
            await context.AddRangeAsync(vendorsSeed);
            await context.SaveChangesAsync();
        }
    }
}
