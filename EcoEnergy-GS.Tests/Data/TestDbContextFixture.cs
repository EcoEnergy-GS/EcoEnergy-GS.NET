using EcoEnergy_GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoEnergy_GS.Tests.Data
{
    public class TestDbContextFixture : IDisposable
    {
        public AppDbContext Context { get; private set; }

        public TestDbContextFixture()
        {
            Context = DbContextFactory.CreateInMemoryDbContext();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
