using AutoMail.Services.impl;
using AutoMail.Services.Interfaces;
using Hangfire;

namespace AutoMail.Services.Implementations
{
    public class BackgroundTaskManager
    {
        private readonly IServiceProvider _serviceProvider;

        public BackgroundTaskManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ScheduleBackgroundTasks()
        {
            // 获取所有的后台任务类型
            var backgroundTaskTypes = new[] { typeof(MailBackgroundTask) }; // 添加更多的任务类型

            var backgroundJobs = _serviceProvider.GetRequiredService<IBackgroundJobClient>();

            foreach (var taskType in backgroundTaskTypes)
            {
                // 根据任务类型创建任务实例并调度
                var taskInstance = (IBackgroundTask)_serviceProvider.GetRequiredService(taskType);
                backgroundJobs.Enqueue(() => taskInstance.ScheduleBackgroundExecute());
            }
        }
    }
}
