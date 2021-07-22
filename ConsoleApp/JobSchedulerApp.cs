using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace ConsoleApp
{
    class JobSchedulerApp
    {
        static async Task Main(string[] args)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            IJobDetail fiveSecondsJob = JobBuilder.Create<FiveSecondsJob>()
                                                    .WithIdentity("five_seconds_job", "seconds_job")
                                                    .Build();

            ITrigger secondsTrigger = TriggerBuilder.Create()
                                                    .WithIdentity("seconds_trigger", "seconds_job")
                                                    .StartNow()
                                                    .WithSimpleSchedule(a => a
                                                        .WithIntervalInSeconds(5)
                                                        .RepeatForever())
                                                    .Build();

            await scheduler.ScheduleJob(fiveSecondsJob, secondsTrigger);

            await Task.Delay(TimeSpan.FromSeconds(60));

            await scheduler.Shutdown();
        }
    }
}
