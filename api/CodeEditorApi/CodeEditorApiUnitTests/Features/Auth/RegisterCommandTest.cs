using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Features.Auth.Register;
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
    public class RegisterCommandTest : UnitTest<RegisterCommand>
    {
        [Fact]
        public async Task ShouldReturnBadRequestIfUserExists()
        {
            var body = fixture.Create<RegisterBody>();
            var user = fixture.Create<User>();
            var expected = new BadRequestError("User with email already exists");

            Freeze<IGetUser>()
                .Setup(x => x.ExecuteAsync(body.Email))
                .ReturnsAsync(user);

            var actionResult = await Target().ExecuteAsync(body);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);

            Freeze<IGetUser>().Verify(x => x.ExecuteAsync(body.Email), Times.Once);
        }

        [Fact]
        public async Task ShouldReturnTokenIfUserDoesNotExist()
        {
            var body = fixture.Create<RegisterBody>();
            var role = It.IsInRange<int>(1, 3, Range.Inclusive);
            var user = fixture.Create<User>();
            var token = fixture.Create<string>();

            Freeze<IGetUser>()
                .Setup(x => x.ExecuteAsync(body.Email))
                .ReturnsAsync((User)null);

            Freeze<IRegister>()
                .Setup(x => x.ExecuteAsync(body))
                .ReturnsAsync(user);

            Freeze<IJwtService>()
                .Setup(x => x.GenerateToken(Freeze<IConfiguration>().Object, user))
                .Returns(token);

            var actionResult = await Target().ExecuteAsync(body);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().Be(token);

            Freeze<IGetUser>().Verify(x => x.ExecuteAsync(body.Email), Times.Once);
            Freeze<IRegister>().Verify(x => x.ExecuteAsync(body), Times.Once);
            Freeze<IJwtService>().Verify(x => x.GenerateToken(Freeze<IConfiguration>().Object, user), Times.Once);
        }

        // We test the validation of the model, since it is prety much testing the Regular Expression Used

        [Theory]
        [InlineData("SecurePassword1!")]
        [InlineData("LsdSKJ349834@$93845")]
        [InlineData("abcdefgsLkdjf12@#")]

        public void ShouldReturnTrueForProperPasswords(string password)
        {
            var body = fixture.Build<RegisterBody>()
                .With(x => x.Password, password)
                .Create();

            var result = ModelValidation.ValidateModel(body, "Password");

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("short1!")]
        [InlineData("NoSymbols1")]
        [InlineData("NOLOWERCASE1!")]
        [InlineData("nouppercase1!")]

        public void ShouldReturnFalseForPasswordsNotMeetingValidation(string password)
        {
            var body = fixture.Build<RegisterBody>()
                .With(x => x.Password, password)
                .Create();

            var result = ModelValidation.ValidateModel(body, "Password");

            result.Should().BeFalse();
        }
    }
}
