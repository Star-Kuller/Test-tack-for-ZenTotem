namespace ZenTotem.Tests
{
    public class UpdateCommandTests
    {

        [Fact]
        public void Execute_WithLessThan2Arguments_ShouldThrowException()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var outputMock = new Mock<IOutput>();
            repositoryMock.Setup(x => x.GetAll()).Returns(new List<Employee>()
            {
                new Employee(0){FirstName = "Jon", LastName = "Doe", Salary = 123.45m},
                new Employee(1){FirstName = "Harry", LastName = "Potter",Salary = 10000},
                new Employee(2){FirstName = "God", LastName = "", Salary = 0.13m}
            });
            var command = new UpdateCommand(repositoryMock.Object, outputMock.Object);
            var arguments = new List<string>() { "id:1", "SomeArgument:Bob" };

            // Act & Assert
            Assert.Throws<Exception>(() => command.Execute(arguments));
        }

        [Fact]
        public void Execute_UpdateWithEmptyJson_ShouldThrowException()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var outputMock = new Mock<IOutput>();
            var command = new UpdateCommand(repositoryMock.Object, outputMock.Object);
            var arguments = new List<string>() { "id:1", "LastName:Trevor" };
            
            repositoryMock.Setup(x => x.GetAll()).Returns(new List<Employee>());
            outputMock.Setup(x => x.Send(It.IsAny<string>()));

            Assert.Throws<Exception>(() => command.Execute(arguments));
        }
        
        [Theory]
        [InlineData("ID:1", "FirstName:Leo")]
        [InlineData("ID:2", "LastName:Granger")]
        [InlineData("ID:0", "FirstName:Bob", "Salary:12,42")]
        public void Execute_WithValidArgument_ShouldUpdateEmployeeAndSendOutput(params string[] arg)
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var outputMock = new Mock<IOutput>();
            var command = new UpdateCommand(repositoryMock.Object, outputMock.Object);
            repositoryMock.Setup(x => x.GetAll()).Returns(new List<Employee>()
                {
                    new Employee(0){FirstName = "Jon", LastName = "Doe", Salary = 123.45m},
                    new Employee(1){FirstName = "Harry", LastName = "Potter",Salary = 10000},
                    new Employee(2){FirstName = "God", LastName = "", Salary = 0.13m}
                });
            repositoryMock.Setup(x => x.Get(0)).Returns(
                new Employee(0){FirstName = "Jon", LastName = "Doe", Salary = 123.45m});
            repositoryMock.Setup(x => x.Get(1)).Returns(
                new Employee(1){FirstName = "Harry", LastName = "Potter",Salary = 10000});
            repositoryMock.Setup(x => x.Get(2)).Returns(
                new Employee(2){FirstName = "God", LastName = "", Salary = 0.13m});
            var arguments = arg.ToList();
            outputMock.Setup(x => x.Send(It.IsAny<string>()));

            // Act
            command.Execute(arguments);

            // Assert
            if(arg[0] == "ID:1") 
                repositoryMock.Verify(x => x.Update(It.Is<Employee>
                    (e => e.FirstName == "Leo")), Times.Once);
            if(arg[0] == "ID:2") 
                repositoryMock.Verify(x => x.Update(It.Is<Employee>
                    (e => e.LastName == "Granger")), Times.Once);
            if(arg[0] == "ID:0") 
                repositoryMock.Verify(x => x.Update(It.Is<Employee>
                    (e => e.FirstName == "Bob" && e.Salary == 12.42m)), Times.Once);
            outputMock.Verify(x => x.Send($"Updated employee {arg[0]}"), Times.Once);
        }
    }
}