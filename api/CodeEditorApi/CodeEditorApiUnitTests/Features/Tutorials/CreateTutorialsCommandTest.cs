using AutoFixture;
using CodeEditorApi.Errors;
using CodeEditorApi.Features.Tutorials.CreateTutorials;
using CodeEditorApiDataAccess.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeEditorApiUnitTests.Features.Tutorials
{
    public class CreateTutorialsCommandTest
    {
        private readonly ICreateTutorialsCommand _target;
        private readonly Fixture _fixture;

        private readonly Mock<ICreateTutorials> _createTutorialsMock;
        
        public CreateTutorialsCommandTest()
        {
            _createTutorialsMock = new Mock<ICreateTutorials>();

            _fixture = new Fixture();

            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _target = new CreateTutorialsCommand(_createTutorialsMock.Object);
        }

        [Fact]
        public async Task ShouldReturnTutorial()
        {
            var body = _fixture.Create<CreateTutorialsBody>();
            var tutorial = _fixture.Create<Tutorial>();

            var actionResult = await _target.ExecuteAsync(body);

            actionResult.Should().BeEquivalentTo(tutorial);
        }
    }
}
