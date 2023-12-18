using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NativeLibrary;

namespace CSharpApp;

public class MainWorker(IServiceProvider sp) : BackgroundService
{
    private bool _disposed;

    private readonly List<LoadGenerator> _loadGenerators = [
        sp.GetRequiredService<LoadGenerator>(),
        sp.GetRequiredService<LoadGenerator>(),
        sp.GetRequiredService<LoadGenerator>(),
        sp.GetRequiredService<LoadGenerator>(),
    ];

    protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;

    ~MainWorker() => Dispose();
    public override void Dispose()
    {
        if (_disposed)
            return;
        _disposed = true;

        foreach (var loadGenerator in _loadGenerators)
        {
            loadGenerator.Dispose();
        }
        _loadGenerators.Clear();

        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
