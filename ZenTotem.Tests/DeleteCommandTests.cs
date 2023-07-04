namespace ZenTotem.Tests
{
    public class DeleteCommandTests
    {

        [Fact]
        public void Execute_WithOutArgument_ShouldThrowException()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var outputMock = new Mock<IOutput>();
            var command = new DeleteCommand(repositoryMock.Object, outputMock.Object);
            var arguments = new List<string>();
            repositoryMock.Setup(x => x.GetAll()).Returns(new List<Employee>()
            {
                new Employee(0){FirstName = "Jon", LastName = "Doe", Salary = 123.45m},
                new Employee(1){FirstName = "Harry", LastName = "Potter",Salary = 10000},
                new Employee(2){FirstName = "God", LastName = "", Salary = 0.13m}
            });

            // Act & Assert
            Assert.Throws<Exception>(() => command.Execute(arguments));
        }

        [Fact]
        public void Execute_WithEmptyJson_ShouldThrowException()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var outputMock = new Mock<IOutput>();
            var command = new DeleteCommand(repositoryMock.Object, outputMock.Object);
            var arguments = new List<string>() {"id:0"};
            repositoryMock.Setup(x => x.GetAll()).Returns(new List<Employee>());
            // Act & Assert
            Assert.Throws<Exception>(() => command.Execute(arguments));
        }
        
        [Fact]
        public void Execute_WithValidArgument_ShouldDeleteEmployeeAndSendOutput()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var outputMock = new Mock<IOutput>();
            var command = new DeleteCommand(repositoryMock.Object, outputMock.Object);
            var arguments = new List<string>() { "id:1" };
            repositoryMock.Setup(x => x.GetAll()).Returns(new List<Employee>()
                {
                    new Employee(0){FirstName = "Jon", LastName = "Doe", Salary = 123.45m},
                    new Employee(1){FirstName = "Harry", LastName = "Potter",Salary = 10000},
                    new Employee(2){FirstName = "God", LastName = "", Salary = 0.13m}
                });

            outputMock.Setup(x => x.Send(It.IsAny<string>()));

            // Act
            command.Execute(arguments);

            // Assert
            repositoryMock.Verify(x => x.Delete(1), Times.Once);
            outputMock.Verify(x => x.Send("Deleted employee ID:1"), Times.Once);
        }
    }
}