using ZenTotem.Core.Parser;
using ZenTotem.Infrastructure;

var parser = new Parser();
var diContainer = new DiContainer();

diContainer.Configure();
parser.Parse(args);