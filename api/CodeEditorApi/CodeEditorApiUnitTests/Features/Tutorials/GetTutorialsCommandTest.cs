using AutoFixture;
using CodeEditorApi.Features.Tutorials.GetTutorials;
using CodeEditorApiDataAccess.Data;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Tutorials
{
    public class GetTutorialsCommandTest
    {
        private readonly IGetTutorialsCommand _target;
        private readonly Fixture _fixture;

        private readonly Mock<IGetTutorials> _getTutorialsMock;

        public GetTutorialsCommandTest()
        {
            _getTutorialsMock = new Mock<IGetTutorials>();

            _fixture = new Fixture();

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _target = new GetTutorialsCommand(_getTutorialsMock.Object);
        }

        [Fact]
        public async Task ShouldReturnTutorial()
        {
            var tutorial = _fixture.Create<Tutorial>();

            var actionResult = await _target.ExecuteAsync(tutorial.Id);

            actionResult.Should().BeEquivalentTo(tutorial);
        }
    }
}
