using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadRunner;

internal sealed class FakeExecutor(int workers) : Executor(workers)
{
    public override async Task ProcessAsync(CancellationToken cancellationToken)
    {
        var value = Random.Shared.Next(3)*1000;
        Thread.Sleep(value);

        Console.WriteLine("The machine has {0} threads with {1} completed work itens"
            , ThreadPool.ThreadCount
            , ThreadPool.CompletedWorkItemCount);

        await Task.CompletedTask;
    }
}
