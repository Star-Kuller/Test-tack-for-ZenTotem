namespace ZenTotem.Tests
{
    public class AddCommandTests
    {

        [Fact]
        public void Execute_WithLessThan3Arguments_ShouldThrowException()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var outputMock = new Mock<IOutput>();
            var command = new AddCommand(repositoryMock.Object, outputMock.Object);
            var arguments = new List<string>() { "SomeArgument:Bob", "LastName:Trevor" };

            // Act & Assert
            Assert.Throws<Exception>(() => command.Execute(arguments));
        }

        [Fact]
        public void Execute_AddsEmployeeAndSendsOutputWithEmptyJson()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var outputMock = new Mock<IOutput>();
            var command = new AddCommand(repositoryMock.Object, outputMock.Object);
            var arguments = new List<string>() { "FirstName:Bob", "LastName:Trevor", "Salary:256,16" };
            repositoryMock.Setup(x => x.GetAll()).Returns(new List<Employee>());
            outputMock.Setup(x => x.Send(It.IsAny<string>()));

            // Act
            command.Execute(arguments);

            // Assert
            repositoryMock.Verify(x => x.Add(It.IsAny<Employee>()), Times.Once);
            outputMock.Verify(x => x.Send("Added employee ID:0"), Times.Once);
        }
        
        [Fact]
        public void Execute_WithValidArgument_ShouldAddEmployeeAndSendOutput()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var outputMock = new Mock<IOutput>();
            var command = new AddCommand(repositoryMock.Object, outputMock.Object);
            var arguments = new List<string>() { "FirstName:Bob", "LastName:Trevor", "Salary:256,16" };
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
            repositoryMock.Verify(x => x.Add(It.Is<Employee>
                (e => e.FirstName == "Bob" && e.LastName == "Trevor" && e.Salary == 256.16m)), Times.Once);
            outputMock.Verify(x => x.Send("Added employee ID:3"), Times.Once); }
        
        [Fact]
        public void Execute_WithNegativeSalary_ShouldAddEmployeeAndSendOutput()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var outputMock = new Mock<IOutput>();
            var command = new AddCommand(repositoryMock.Object, outputMock.Object);
            var arguments = new List<string>() { "FirstName:Bob", "LastName:Trevor", "Salary:-256,16" };
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
            repositoryMock.Verify(x => x.Add(It.Is<Employee>
                (e => e.FirstName == "Bob" && e.LastName == "Trevor" && e.Salary == 0m)), Times.Once);
            outputMock.Verify(x => x.Send("Added employee ID:3"), Times.Once); }
    }
}