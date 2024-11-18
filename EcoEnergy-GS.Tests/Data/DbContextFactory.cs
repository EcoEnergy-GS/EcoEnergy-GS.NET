using EcoEnergy_GS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoEnergy_GS.Tests.Data
{
    public class DbContextFactory
    {
        public static AppDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            var context = new AppDbContext(options);
            return context;
        }
    }
}
