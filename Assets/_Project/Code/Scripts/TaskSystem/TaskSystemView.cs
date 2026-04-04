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
        [SerializeField] private TMP_Text _rewardText;
        private TaskIconConfig _iconConfig;

        private ITaskService _taskService;

        public void ManualAwake(ITaskService taskService, TaskIconConfig iconConfig)
        {
            _taskService = taskService;
            _iconConfig = iconConfig;
            _taskService.OnTaskStarted += UpdateView;
            _taskService.OnTaskCompleted += OnTaskCompleted;

            if (_taskService.HasActiveTask)
                UpdateView(_taskService.CurrentTask.Value);
            else
                ClearView();
        }

        private void OnDestroy()
        {
            if (_taskService != null)
            {
                _taskService.OnTaskStarted -= UpdateView;
                _taskService.OnTaskCompleted -= OnTaskCompleted;
            }
        }

        private void UpdateView(TaskData task)
        {
            _titleText.enabled = true;
            _titleText.text = task.ResultType.ToDisplayString();

            var icon = _iconConfig.GetIcon(task.ResultType);
            _iconImage.sprite = icon;
            _iconImage.enabled = icon != null;

            _rewardText.enabled = true;
            _rewardText.text = task.CreditReward.ToString();
        }

        private void OnTaskCompleted(TaskData _)
        {
            if (!_taskService.HasActiveTask)
                ClearView();
        }

        private void ClearView()
        {
            _titleText.text = string.Empty;
            _titleText.enabled = false;
            _iconImage.sprite = null;
            _iconImage.enabled = false;
            _rewardText.text = string.Empty;
            _rewardText.enabled = false;
        }
    }
}