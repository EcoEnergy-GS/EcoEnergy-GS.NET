using EcoEnergy_GS.Data;

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
