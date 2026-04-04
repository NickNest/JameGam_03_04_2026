using _Project.Code.Scripts.Configs;
using _Project.Code.Scripts.Data.TaskData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Scripts.TaskSystem
{
    public class TaskSystemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TaskIconConfig _iconConfig;

        private ITaskService _taskService;

        public void Initialize(ITaskService taskService)
        {
            _taskService = taskService;
            _taskService.OnTaskStarted += UpdateView;

            if (_taskService.HasActiveTask)
                UpdateView(_taskService.CurrentTask.Value);
        }

        private void OnDestroy()
        {
            if (_taskService != null)
                _taskService.OnTaskStarted -= UpdateView;
        }

        private void UpdateView(TaskData task)
        {
            _titleText.text = task.ResultType.ToDisplayString();

            var icon = _iconConfig.GetIcon(task.ResultType);
            _iconImage.sprite = icon;
            _iconImage.enabled = icon != null;
        }
    }
}