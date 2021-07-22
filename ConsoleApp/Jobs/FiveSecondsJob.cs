using System;
using System.Threading.Tasks;
using Quartz;

namespace ConsoleApp
{
    public class FiveSecondsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"FiveMinutesJob executed at {DateTimeOffset.Now}");
        }
    }
}