using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadRunner;

internal abstract class Executor(int workers)
{
    private readonly int _workers = workers;

    public abstract Task ProcessAsync(CancellationToken cancellationToken);

    public async Task Execute(CancellationToken cancellationToken)
    {
        var tasks = new List<Func<Task>>();
        
        for (var  i = 0; i < _workers; i++)
        {
            var task = new Func<Task>(async () => {
                while (true)
                {
                    try
                    {
                        await ProcessAsync(cancellationToken);
                        await Task.Delay(1000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        await Task.Delay(10000);
                    }
                }
            });

            tasks.Add(task);
        }

        await Task.WhenAll(tasks.Select(t => t()).ToArray());
    }
}
