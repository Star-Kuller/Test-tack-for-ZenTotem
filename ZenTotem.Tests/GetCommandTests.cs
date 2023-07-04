namespace ZenTotem.Tests
{
    public class GetCommandTests
    {
        
        [Fact]
        public void Execute_WithOutArgument_ShouldThrowException()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var outputFormatterMock = new Mock<IOutputFormatter>();
            var outputMock = new Mock<IOutput>();
            var command = new GetCommand(repositoryMock.Object, outputFormatterMock.Object, outputMock.Object);
            var arguments = new List<string>();
            repositoryMock.Setup(x => x.GetAll()).Returns(new List<Employee>()
            {
                new Employee(0) { FirstName = "Jon", LastName = "Doe", Salary = 123.45m },
                new Employee(1) { FirstName = "Harry", LastName = "Potter", Salary = 10000 },
                new Employee(2) { FirstName = "God", LastName = "", Salary = 0.13m }
            });

            // Act & Assert
            Assert.Throws<Exception>(() => command.Execute(arguments));
        }
        
        [Fact]
        public void Execute_ShouldSendString()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var outputFormatterMock = new Mock<IOutputFormatter>();
            var outputMock = new Mock<IOutput>();
            var command = new GetCommand(repositoryMock.Object, outputFormatterMock.Object, outputMock.Object);
            var arguments = new List<string>() {"id: 1"};
            repositoryMock.Setup(x => x.GetAll()).Returns(new List<Employee>()
            {
                new Employee(0) { FirstName = "Jon", LastName = "Doe", Salary = 123.45m },
                new Employee(1) { FirstName = "Harry", LastName = "Potter", Salary = 10000 },
                new Employee(2) { FirstName = "God", LastName = "", Salary = 0.13m }
            });
            outputFormatterMock.Setup(x => x.CreateForOneObject(It.IsAny<Employee>()))
                .Returns("TestTable");
            outputMock.Setup(x => x.Send(It.IsAny<string>()));

            // Act
            command.Execute(arguments);

            // Assert
            outputFormatterMock.Verify(x => x.CreateForOneObject(It.IsAny<Employee>()), Times.Once);
            outputMock.Verify(x => x.Send("TestTable"), Times.Once);
        }

    }
}