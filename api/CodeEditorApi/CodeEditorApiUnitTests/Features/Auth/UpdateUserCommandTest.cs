using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Features.Auth.UpdateUser;
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
    public class UpdateUserCommandTest : UnitTest<UpdateUserCommand>
    {
        [Fact]
        public async Task ShouldReturnBadRequestIfInvalidNewUsername()
        {
            var name = "otherusernam";
            var oldName = "originalusername";

            var otherUser = fixture.Build<User>()
                .With(u => u.Name, name)
                .Create();
            var testUser = fixture.Build<User>()
                .With(u => u.Name, oldName)
                .Create();

            var uub = fixture.Build<UpdateUserBody>()
                .With(ub => ub.Name, name)
                .Create();

            var expected = new BadRequestError($"Could not update User's username because another account already exists with username {uub.Name}");

            Freeze<IGetUser>().Setup(gu => gu.GetUserByName(uub.Name)).ReturnsAsync(otherUser);

            var actionResult = await Target().ExecuteAsync(uub);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfInvalidNewEmail()
        {           
            var email = "other@email.com";
            var oldEmail = "unittest@email.com";

            var otherUser = fixture.Build<User>()
                .With(u => u.Email, oldEmail)
                .Create();
            var testUser = fixture.Build<User>()
                .With(u => u.Email, oldEmail)
                .Create();

            var uub = fixture.Build<UpdateUserBody>()
                .With(ub => ub.Id, testUser.Id)
                .With(ub => ub.Name, testUser.Name)
                .With(ub => ub.Email, email)
                .Create();

            var expected = new BadRequestError($"Could not update User's email because another account already exists with email {email}");

            Freeze<IGetUser>().Setup(gu => gu.GetUserInfo(uub.Id)).ReturnsAsync(testUser);
            Freeze<IGetUser>().Setup(gu => gu.GetUserByName(uub.Name)).ReturnsAsync((User)null);
            Freeze<IGetUser>().Setup(gu => gu.ExecuteAsync(email)).ReturnsAsync(otherUser);

            var actionResult = await Target().ExecuteAsync(uub);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfInvalidUserId()
        {
            var id = It.IsAny<int>();

            var uub = fixture.Build<UpdateUserBody>()
                .With(ub => ub.Id, id)
                .Create();

            var expected = new BadRequestError($"Could not find a User with ID {id}");

            Freeze<IGetUser>().Setup(gu => gu.GetUserInfo(id)).ReturnsAsync((User)null);

            var actionResult = await Target().ExecuteAsync(uub);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfOldPassDoesNotMatchCurrent()
        {
            var user = fixture.Create<User>();

            var uub = fixture.Build<UpdateUserBody>()
                .With(ub => ub.Name, user.Name)
                .With(ub => ub.Id, user.Id)
                .With(ub => ub.Email, user.Email)
                .Create();

            var expected = new BadRequestError($"user input for current password does not match password saved in database");

            Freeze<IGetUser>().Setup(gu => gu.GetUserByName(uub.Name)).ReturnsAsync((User)null);
            Freeze<IGetUser>().Setup(gu => gu.GetUserInfo(uub.Id)).ReturnsAsync(user);
            Freeze<IHashService>().Setup(hs => hs.ComparePassword(user.Hash, uub.OldPassword)).Returns(false);

            var actionResult = await Target().ExecuteAsync(uub);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfNewPassIsEmpty()
        {
            var user = fixture.Create<User>();

            var uub = fixture.Build<UpdateUserBody>()
                .With(ub => ub.Id, user.Id)
                .With(ub => ub.Name, user.Name)
                .With(ub => ub.Email, user.Email)
                .With(ub => ub.NewPassword, "")
                .Create();

            var expected = new BadRequestError($"a new password must be provided to update a password.");

            Freeze<IGetUser>().Setup(gu => gu.GetUserInfo(user.Id)).ReturnsAsync(user);

            Freeze<IHashService>().Setup(hs => hs.ComparePassword(user.Hash, uub.OldPassword)).Returns(true);

            var actionResult = await Target().ExecuteAsync(uub);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ShouldReturnTokenIfUpdateUserBodyIsOk()
        {
            var user = fixture.Create<User>();

            var uub = fixture.Build<UpdateUserBody>()
                .With(ub => ub.Name, user.Name)
                .With(ub => ub.Id, user.Id)
                .With(ub => ub.Email, user.Email)
                .With(ub => ub.NewPassword, "goodPass1234!")
                .Create();

            var updateUser = fixture.Build<User>()
                .With(u => u.Id, user.Id)
                .With(u => u.Name, user.Name)
                .With(u => u.Email, user.Email)
                .Create();

            var token = fixture.Create<string>();

            Freeze<IGetUser>().Setup(gu => gu.GetUserByName(uub.Name)).ReturnsAsync(user);

            Freeze<IGetUser>().Setup(gu => gu.GetUserInfo(user.Id)).ReturnsAsync(user);

            Freeze<IHashService>().Setup(hs => hs.ComparePassword(user.Hash, uub.OldPassword)).Returns(true);

            Freeze<IUpdateUser>().Setup(uu => uu.ExecuteAsync(uub)).ReturnsAsync(updateUser);

            Freeze<IJwtService>().Setup(jwt => jwt.GenerateToken(Freeze<IConfiguration>().Object, updateUser)).Returns(token);

            var actionResult = await Target().ExecuteAsync(uub);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().Be(token);
        }
    }
}
