using Abp.Dependency;
using Abp.Events.Bus.Handlers;

namespace BeiDreamAbp.Service.Tasks
{
    /// <summary>
    /// ABP扫描所有实现IEventHandler接口的类,并且自动注册它们到事件总线中。当事件发生, 它通过依赖注入(DI)来取得处理器(handler)的引用对象并且在事件处理完毕之后将其释放
    /// </summary>
    public class CreateOrUpdateTasksHandler : IEventHandler<TaskCompletedEventData>, ITransientDependency 
    {
        public void HandleEvent(TaskCompletedEventData eventData)
        {
            var aa = eventData.TaskId;
        }
    }
}