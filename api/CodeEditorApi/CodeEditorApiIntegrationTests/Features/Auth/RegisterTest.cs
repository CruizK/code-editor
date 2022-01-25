using AutoFixture;
using CodeEditorApi.Features.Auth.Register;
using CodeEditorApiDataAccess.StaticData;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiIntegrationTests.Features.Auth
{
    public class RegisterTest : DbTest<Register>
    {
        [Fact]
        public async Task ShouldCreateUser()
        {
            using (var context = TestContext())
            {
                // Assemble
                target = new Register(context);
                var body = fixture.Create<RegisterBody>();
                int role = (int)fixture.Create<Roles>();

                // Execute
                var user = await target.ExecuteAsync(body, role);


                // Assert
                context.Users.Count().Should().Be(1);
                context.Users.Where(x => x.Id == user.Id).First().Should().BeEquivalentTo(user);
            }
        }
    }
}
