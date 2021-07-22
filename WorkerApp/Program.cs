using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace WorkerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddQuartz(q =>
                    {
                        q.ScheduleJob<HelloJob>(trigger => trigger
                            .WithIdentity("hello_job")
                            .StartNow()
                            .WithDailyTimeIntervalSchedule(interval => interval
                                .WithIntervalInSeconds(5))
                            .WithDescription("Job configured for tests!")); 
                    });

                    services.AddQuartzHostedService(options =>
                        options.WaitForJobsToComplete = true);
                    
                    services.AddHostedService<Worker>();
                });
    }
}
