using CSharpApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NativeLibrary;

var hostBuilder = Host.CreateApplicationBuilder(args);
hostBuilder.Configuration
    .AddCommandLine(args);
hostBuilder.Services
    .AddLogging(builder => builder
        .SetMinimumLevel(LogLevel.Trace)
        .AddSimpleConsole(options => options.SingleLine = true)
    )
    .AddTransient<LoadGenerator>()
    .AddHostedService<MainWorker>()
    .BuildServiceProvider();
using var host = hostBuilder.Build();

Hello.PrintHelloWorld();

host.Run();
