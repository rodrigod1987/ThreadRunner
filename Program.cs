using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadRunner;

public class Program
{
    private static int _workers => 100;
    private static int _threads => 10;

    static Program()
    {
        ThreadPool.SetMaxThreads(_threads, _threads * 2);
    }

    public static async Task Main(string[] args)
    {
        var fakeExecutor = new FakeExecutor(_workers);
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token;
        await fakeExecutor.Execute(cancellationToken);
        Console.WriteLine("Executor finished");
    }
}