using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetAccess;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApiDataAccess.Data;
using CodeEditorApiDataAccess.StaticData;
using CodeEditorApiUnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Auth
{
    public class GetAccessCommandTest : UnitTest<GetAccessCommand>
    {
        [Fact]
        public async Task ShouldReturnAccessCode()
        {
            var user = fixture.Build<User>()
                .With(u => u.RoleId, (int)Roles.Admin)
                .Create();
            Guid uuid = fixture.Create<Guid>();

            Freeze<IGetUser>().Setup(gu => gu.GetUserInfo(user.Id)).ReturnsAsync(user);
            Freeze<IGetAccess>().Setup(ga => ga.ExecuteAsync()).ReturnsAsync(uuid);

            var actionResult = await Target().ExecuteAsync(user.Id);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().Be(uuid);
        }

        [Theory]
        [InlineData((int)Roles.Teacher)]
        [InlineData((int)Roles.Student)]
        public async Task ShouldReturnBadRequestIfUserIsUnAuthorized(int role)
        {
            var user = fixture.Build<User>()
                .With(u => u.RoleId, role)
                .Create();

            var expected = new BadRequestError($"User {user.Id} is not an Admin. Only Admin are allowed to generate access codes.");

            Freeze<IGetAccess>().Setup(ga => ga.ExecuteAsync()).ReturnsAsync(null);

            var actionResult = await Target().ExecuteAsync(user.Id);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);


        }

    }
}
