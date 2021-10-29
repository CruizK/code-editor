using AutoFixture;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApiDataAccess.Data;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiIntegrationTests.Features.Auth
{
    public class GetUserTest : DbTest<GetUser>
    {
        [Fact]
        public async Task ShouldFindUserGivenEmail()
        {
            using (var context = TestContext())
            {
                // Assemble
                target = new GetUser(context);
                var email = fixture.Create<string>();

                var expectedUser = fixture.Build<User>()
                    .With(x => x.Email, email)
                    .Create();

                var users = fixture.CreateMany<User>();

                context.Users.Add(expectedUser);
                context.Users.AddRange(users);
                context.SaveChanges();

                // Execute
                var user = await target.ExecuteAsync(email);


                // Assert
                user.Should().BeEquivalentTo(expectedUser);
            }
        }

        [Fact]
        public async Task ShouldReturnNullIfNoUserWithEmail()
        {
            using (var context = TestContext())
            {
                // Assemble
                target = new GetUser(context);
                var email = fixture.Create<string>();

                var users = fixture.CreateMany<User>();
                var allUsers = context.Users.ToList();
                context.Users.AddRange(users);
                context.SaveChanges();

                // Execute
                var user = await target.ExecuteAsync(email);


                // Assert
                user.Should().BeNull();
            }
        }
    }
}
