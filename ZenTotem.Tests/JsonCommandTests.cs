namespace ZenTotem.Tests
{
    public class JsonCommandTests
    {
        [Fact]
        public void Execute_WithInvalidSyntaxArgument_ThrowsException()
        {
            //Arrange
            var outputMoq = new Mock<IOutput>();
            var invalidArg = "invalidArg";
            var command = new JsonCommand(outputMoq.Object);

            //Act & Assert
            Assert.Throws<Exception>(() => command.Execute(new List<string> { invalidArg }));
        }
    }
}