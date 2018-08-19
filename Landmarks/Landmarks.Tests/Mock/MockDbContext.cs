using System;
using Landmarks.Data;
using Microsoft.EntityFrameworkCore;

namespace Landmarks.Tests.Mock
{
    public static class MockDbContext
    {
        public static LandmarksDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<LandmarksDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var dbContext = new LandmarksDbContext(options);

            return dbContext;
        }
    }
}
