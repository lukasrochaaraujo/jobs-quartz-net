using System;
using System.Threading.Tasks;
using Quartz;

namespace WorkerApp
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"HelloJob executed at {DateTimeOffset.Now}");
        }
    }
}