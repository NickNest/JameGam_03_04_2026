using System;
using _Project.Code.Scripts.Data.TaskData;

namespace _Project.Code.Scripts.TaskSystem
{
    public interface ITaskService
    {
        event Action<TaskData> OnTaskStarted;
        event Action<TaskData> OnTaskCompleted;

        bool HasActiveTask { get; }
        TaskData? CurrentTask { get; }

        void CompleteCurrentTask();
    }
}