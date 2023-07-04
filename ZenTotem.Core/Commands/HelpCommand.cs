using ZenTotem.Infrastructure;

namespace ZenTotem.Core;

public class HelpCommand : ICommand
{
    private readonly IOutput _output;

    public HelpCommand(IOutput output)
    {
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }

    private const string HelpMessage =
        "\n-help - Prints a list of commands to the console.\n" +
        "-json path:{path} [name:{name}] - Specifies the path to the json file or creates one.\n" +
        "-get id:{id} - Displays employee with id == {id}\n" +
        "-getAll - Displays all employees" +
        "-add FirstName:{FirstName} LastName:{LastName} Salary:{Salary} - " +
        "Add an employee with the specified parameters.\n" +
        "-delete id:{id} - Delete Employee with id == {id}\n" +
        "-update id:{id} [FirstName:{FirstName}] [LastName:{LastName}] [Salary:{Salary}]" +
        " - Update employee information with id == {id}\n";
    public void Execute(List<string> arguments)
    {
        _output.Send(HelpMessage);
    }
}