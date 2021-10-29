using AutoFixture;
using CodeEditorApiDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CodeEditorApiIntegrationTests
{
    public class DbTest<T>
    {
        private readonly DbContextOptions<CodeEditorApiContext> _options;
        protected IFixture fixture;
        protected T target;

        public DbTest()
        {
            fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _options = new DbContextOptionsBuilder<CodeEditorApiContext>()
                .UseInMemoryDatabase("Test", new InMemoryDatabaseRoot())
                .Options;
        }


        protected CodeEditorApiContext TestContext()
        {
            return new CodeEditorApiContext(_options);
        }
    }
}
