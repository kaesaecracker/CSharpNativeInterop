using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NativeLibrary;

var sp = new ServiceCollection()
    .AddLogging(builder => builder
        .AddConsole()
        .SetMinimumLevel(LogLevel.Trace))
    .AddTransient<LoadGenerator>()
    .BuildServiceProvider();

var waitMs = 1000;
if (args.Length == 1 && !int.TryParse(args[0], out waitMs))
    Console.Error.WriteLine($"invalid number {args[0]}");


using (_ = sp.GetRequiredService<LoadGenerator>())
using (_ = sp.GetRequiredService<LoadGenerator>())
using (_ = sp.GetRequiredService<LoadGenerator>())
using (_ = sp.GetRequiredService<LoadGenerator>())
{
    Console.WriteLine("main thread sleeps");
    Thread.Sleep(waitMs);
    Console.WriteLine("main thread wakes up");
}

Hello.PrintHelloWorld();
