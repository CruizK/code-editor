using AutoFixture;
using CodeEditorApi.Features.Tutorials.DeleteTutorials;
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
    public class DeleteTutorialsCommandTest
    {
        private readonly IDeleteTutorialsCommand _target;
        private readonly Fixture _fixture;

        private readonly Mock<IDeleteTutorials> _deleteTutorialsMock;

        public DeleteTutorialsCommandTest()
        {
            _deleteTutorialsMock = new Mock<IDeleteTutorials>();

            _fixture = new Fixture();

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _target = new DeleteTutorialsCommand(_deleteTutorialsMock.Object);
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
