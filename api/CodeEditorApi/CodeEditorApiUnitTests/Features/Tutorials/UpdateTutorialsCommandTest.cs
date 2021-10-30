using AutoFixture;
using CodeEditorApi.Features.Tutorials.CreateTutorials;
using CodeEditorApi.Features.Tutorials.UpdateTutorials;
using CodeEditorApiDataAccess.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Tutorials
{
    public class UpdateTutorialsCommandTest
    {
        private readonly IUpdateTutorialsCommand _target;
        private readonly Fixture _fixture;

        private readonly Mock<IUpdateTutorials> _updateTutorialsMock;

        public UpdateTutorialsCommandTest()
        {
            _updateTutorialsMock = new Mock<IUpdateTutorials>();

            _fixture = new Fixture();

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _target = new UpdateTutorialsCommand(_updateTutorialsMock.Object);
        }

        [Fact]
        public async Task ShouldReturnTutorial()
        {
            var body = _fixture.Create<CreateTutorialsBody>();
            var user = _fixture.Create<User>();
            var tutorial = _fixture.Create<Tutorial>();

            var actionResult = await _target.ExecuteAsync(user.Id, body);

            actionResult.Should().BeEquivalentTo(tutorial);
        }
    }
}
