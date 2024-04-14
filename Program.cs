using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadRunner;

public class Program
{
    public static async Task Main(string[] args)
    {
        var fakeExecutor = new FakeExecutor(6);
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
        await fakeExecutor.Execute(cancellationToken);
        Console.WriteLine("Executor finished");
    }
}