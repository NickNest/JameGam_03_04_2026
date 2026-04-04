using System;
using System.Collections.Generic;
using _Project.Code.Scripts.Data.TaskData;
using UnityEngine;

namespace _Project.Code.Scripts.TaskSystem
{
    public class TaskService : ITaskService
    {
        public event Action<TaskData> OnTaskStarted;
        public event Action<TaskData> OnTaskCompleted;

        public bool HasActiveTask => _currentTask.HasValue;
        public TaskData? CurrentTask => _currentTask;

        private readonly List<TaskData> _tasks;
        private int _currentIndex;
        private TaskData? _currentTask;

        public TaskService(List<TaskData> tasks)
        {
            _tasks = tasks;
            StartNext();
        }

        public void CompleteCurrentTask()
        {
            if (!_currentTask.HasValue)
            {
                Debug.LogWarning("[TaskService] No active task to complete.");
                return;
            }

            var completed = _currentTask.Value;
            _currentTask = null;
            OnTaskCompleted?.Invoke(completed);

            StartNext();
        }

        private void StartNext()
        {
            if (_tasks == null || _tasks.Count == 0)
                return;

            if (_currentIndex >= _tasks.Count)
            {
                Debug.Log("[TaskService] All tasks completed.");
                return;
            }

            _currentTask = _tasks[_currentIndex++];
            OnTaskStarted?.Invoke(_currentTask.Value);
        }
    }
}