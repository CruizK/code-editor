using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;

namespace CodeEditorApiUnitTests.Helpers
{
    public class UnitTest<T>
    {
        protected IFixture fixture;

        public UnitTest()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        protected Mock<M> Freeze<M>() where M : class
        {
            return fixture.Freeze<Mock<M>>();
        }

        protected T Target()
        {
            return fixture.Create<T>();
        }
    }
}
