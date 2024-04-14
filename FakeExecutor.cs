using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadRunner;

internal sealed class FakeExecutor(int workers) : Executor(workers)
{
    private static int _executions = 0;

    public override async Task ProcessAsync(CancellationToken cancellationToken)
    {
        Interlocked.Increment(ref _executions);
        if (_executions % 5 == 0)
        {
            Console.WriteLine("The thread with random {0} was not executed", _executions);
            throw new TaskCanceledException();
        }
        Console.WriteLine("The thread with random {0} was executed", _executions);
        await Task.CompletedTask;
    }
}
