using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Auth.GetUser;
using CodeEditorApi.Features.Auth.Register;
using CodeEditorApi.Services;
using CodeEditorApiDataAccess.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Auth
{
    public class RegisterCommandTest
    {

        private readonly RegisterCommand _target;
        private readonly Fixture _fixture;

        private readonly Mock<IRegister> _registerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IGetUser> _getUserMock;
        private readonly Mock<IJwtService> _jwtServiceMock;

        public RegisterCommandTest()
        {
            _registerMock = new Mock<IRegister>();
            _configurationMock = new Mock<IConfiguration>();
            _getUserMock = new Mock<IGetUser>();
            _jwtServiceMock = new Mock<IJwtService>();

            _fixture = new Fixture();

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _target = new RegisterCommand(_registerMock.Object,
                _getUserMock.Object, 
                _configurationMock.Object, 
                _jwtServiceMock.Object);
        }

        [Fact]
        public async Task ShouldReturnBadRequestIfUserExists()
        {
            var body = _fixture.Create<RegisterBody>();
            var user = _fixture.Create<User>();
            var expected = new BadRequestError("User with email already exists");

            _getUserMock
                .Setup(x => x.ExecuteAsync(body.Email))
                .ReturnsAsync(user);

            var actionResult = await _target.ExecuteAsync(body);

            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task ShouldReturnTokenIfUserDoesNotExist()
        {
            var body = _fixture.Create<RegisterBody>();
            var user = _fixture.Create<User>();
            var token = _fixture.Create<string>();

            _getUserMock
                .Setup(x => x.ExecuteAsync(body.Email))
                .ReturnsAsync((User)null);

            _registerMock
                .Setup(x => x.ExecuteAsync(body))
                .ReturnsAsync(user);

            _jwtServiceMock
                .Setup(x => x.GenerateToken(_configurationMock.Object, user))
                .Returns(token);

            var actionResult = await _target.ExecuteAsync(body);

            actionResult.Result.Should().BeNull();
            actionResult.Value.Should().Be(token);
        }
    }
}
