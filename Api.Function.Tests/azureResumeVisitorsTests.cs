using Xunit;
using FluentAssertions;

namespace Api.Function.Tests.Unit
{
    public class azureResumeVisitorsTests
    {
        private readonly azureResumeVisitors _sut;

        public azureResumeVisitorsTests()
        {
            _sut = new azureResumeVisitors(null);  // No need for a logger in this test.
        }

        [Fact]
        public void IncrementCounter_ShouldIncrementCount()
        {
            // Arrange
            var counter = new Counter { Id = "1", Count = 1 };

            // Act
            var result = _sut.IncrementCounter(counter);

            // Assert
            result.Count.Should().Be(2);
        }
    }
}
