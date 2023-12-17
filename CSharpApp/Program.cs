using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NativeLibrary;

var sp = new ServiceCollection()
    .AddLogging(builder => builder
        .SetMinimumLevel(LogLevel.Trace)
        .AddSimpleConsole(options => options.SingleLine = true)
    )
    .AddTransient<LoadGenerator>()
    .BuildServiceProvider();

var logger = sp.GetRequiredService<ILogger<Program>>();

var waitMs = 1000;
if (args.Length == 1 && !int.TryParse(args[0], out waitMs))
    Console.Error.WriteLine($"invalid number {args[0]}");

using (_ = sp.GetRequiredService<LoadGenerator>())
using (_ = sp.GetRequiredService<LoadGenerator>())
using (_ = sp.GetRequiredService<LoadGenerator>())
using (_ = sp.GetRequiredService<LoadGenerator>())
{
    logger.LogInformation("main thread sleeps");
    Thread.Sleep(waitMs);
    logger.LogInformation("main thread wakes up");
}

Hello.PrintHelloWorld();
