namespace AutoMail.Services.Interfaces
{
    public interface IBackgroundTask
    {
        // 所有的任务调度都需要实现这个接口
        void ScheduleBackgroundExecute();
    }
}
