using Microsoft.Extensions.Configuration;

namespace ZenTotem.Infrastructure;
public class DiContainer
{
    public void Configure()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Build();
    }
}