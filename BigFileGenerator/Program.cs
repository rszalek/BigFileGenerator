using BigFileGenerator;
using BigFileGenerator.ConfigurationClasses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

var serviceDescriptors = new ServiceCollection();
ConfigureServices(serviceDescriptors, configuration);
var serviceProvider = serviceDescriptors.BuildServiceProvider();

//application runs here
var appService = serviceProvider.GetService<App>();
var maxLines = configuration.GetSection("OutputFileOptions:MaxLines").Value != null
    ? int.Parse(configuration.GetSection("OutputFileOptions:MaxLines").Value)
    : 1;
if (args.Length > 0)
{
    var isIntegerParam = int.TryParse(args[0], out var num);
    maxLines = isIntegerParam ? num : maxLines;
}
if (appService != null)
{
    await appService.Run(maxLines);
}


static void ConfigureServices(IServiceCollection serviceCollection, IConfigurationRoot config)
{
    serviceCollection.AddSingleton(config);
    serviceCollection.AddTransient<App>();
}



