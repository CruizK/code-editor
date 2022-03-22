using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Features.Auth.Login;
using CodeEditorApi.Services;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Auth
{
    public class LoginCommandTest : UnitTest<LoginCommand>
    {
        [Fact]
        public async Task ShouldReturnBadRequestIfUserDoesNotExist()
        {
            var body = fixture.Create<LoginBody>();
            var user = fixture.Create<User>();
            var expected = new BadRequestError("User does not exist or not verified");

            Freeze<IGetUser>()
                .Setup(x => x.ExecuteAsync(body.Email.ToLower()))
                .ReturnsAsync((User)null);

            var actionResult = await Target().ExecuteAsync(body);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);

            Freeze<IGetUser>().Verify(x => x.ExecuteAsync(body.Email.ToLower()), Times.Once);
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfPasswordDoesNotMatch()
        {
            var body = fixture.Create<LoginBody>();
            var user = fixture.Build<User>().With(x => x.IsConfirmed, true).Create();
            var expected = new BadRequestError("Password was incorrect");

            Freeze<IGetUser>()
                .Setup(x => x.ExecuteAsync(body.Email.ToLower()))
                .ReturnsAsync(user);

            Freeze<IHashService>()
                .Setup(x => x.ComparePassword(user.Hash, body.Password))
                .Returns(false);

            var actionResult = await Target().ExecuteAsync(body);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);

            Freeze<IGetUser>().Verify(x => x.ExecuteAsync(body.Email.ToLower()), Times.Once);
        }

        [Fact]
        public async Task ShouldReturnTokenIfUserExistsAndPasswordMatches()
        {
            var body = fixture.Create<LoginBody>();
            var user = fixture
                .Build<User>()
                .With(x => x.IsConfirmed, true)
                .Create();
            var token = fixture.Create<string>();

            Freeze<IGetUser>()
                .Setup(x => x.ExecuteAsync(body.Email.ToLower()))
                .ReturnsAsync(user);

            Freeze<IHashService>()
                .Setup(x => x.ComparePassword(user.Hash, body.Password))
                .Returns(true);

            Freeze<IJwtService>()
                .Setup(x => x.GenerateToken(Freeze<IConfiguration>().Object, user))
                .Returns(token);

            var actionResult = await Target().ExecuteAsync(body);


            Freeze<IGetUser>().Verify(x => x.ExecuteAsync(body.Email.ToLower()), Times.Once);
            Freeze<IHashService>().Verify(x => x.ComparePassword(user.Hash, body.Password), Times.Once);
            Freeze<IJwtService>().Verify(x => x.GenerateToken(Freeze<IConfiguration>().Object, user), Times.Once);
        }
        
    }
}
