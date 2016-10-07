using Xunit;

namespace Fluent.ValidatorRules.UnitTestExtension.Tests
{
    public class ClassTest
    {
        [Fact]
        public void DummyTest()
        {
            // Arrange

            // Act
            var @class = new Class();

            // Assert
            Assert.NotNull(@class);
        }
    }
}
