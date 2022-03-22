using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Features.Auth.Register;
using CodeEditorApi.Services;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiDataAccess.StaticData;
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

            var result = actionResult as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);

            Freeze<IGetUser>().Verify(x => x.ExecuteAsync(body.Email), Times.Once);
        }

        [Theory]
        [InlineData(null, Roles.Admin)]
        [InlineData("SomeCode", Roles.Admin)]
        [InlineData(null, Roles.Teacher)]
        [InlineData("SomeCode", Roles.Teacher)]
        public async Task ShouldReturnBadRequestIfRoleTeacherAndInvalidAccessCode(string accessCode, Roles role)
        {
            var body = fixture.Build<RegisterBody>()
                .With(x => x.AccessCode, accessCode)
                .With(x => x.Role, role)
                .Create();
            var expected = new BadRequestError("Invalid Access Code");

            Freeze<IGetUser>()
                .Setup(x => x.ExecuteAsync(body.Email.ToLower()))
                .ReturnsAsync((User)null);

            var actionResult = await Target().ExecuteAsync(body);

            var result = actionResult as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);

            Freeze<IGetUser>().Verify(x => x.ExecuteAsync(body.Email.ToLower()), Times.Once);
        }

        /* This test may fail due to race conditions on AccessCodeService, will be fixed when switched to redis */
        [Fact]
        public async Task ShouldReturnBadRequestIfAccessCodeIsForAnotherRole()
        {
            AccessCodeService.ClearCodes();
            var accessCode = AccessCodeService.GenerateAccessCode(Roles.Admin);
            

            var body = fixture.Build<RegisterBody>()
                .With(x => x.AccessCode, accessCode)
                .With(x => x.Role, Roles.Teacher)
                .Create();

            var expected = new BadRequestError("Invalid Access Code");

            Freeze<IGetUser>()
                .Setup(x => x.ExecuteAsync(body.Email.ToLower()))
                .ReturnsAsync((User)null);

                

            var actionResult = await Target().ExecuteAsync(body);

            var result = actionResult as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);

            Freeze<IGetUser>().Verify(x => x.ExecuteAsync(body.Email.ToLower()), Times.Once);

            AccessCodeService.ClearCodes();
        }

        [Fact]
        public async Task ShouldReturnTokenIfUserDoesNotExist()
        {
            var body = fixture.Build<RegisterBody>()
                .With(x => x.Role, Roles.Student)
                .Create();
            var user = fixture.Create<User>();
            var token = fixture.Create<string>();

            Freeze<IGetUser>()
                .Setup(x => x.ExecuteAsync(body.Email.ToLower()))
                .ReturnsAsync((User)null);

            Freeze<IRegister>()
                .Setup(x => x.ExecuteAsync(body))
                .ReturnsAsync(user);

            Freeze<IJwtService>()
                .Setup(x => x.GenerateToken(Freeze<IConfiguration>().Object, user))
                .Returns(token);

            var actionResult = await Target().ExecuteAsync(body);


            Freeze<IGetUser>().Verify(x => x.ExecuteAsync(body.Email.ToLower()), Times.Once);
            Freeze<IRegister>().Verify(x => x.ExecuteAsync(body), Times.Once);
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
