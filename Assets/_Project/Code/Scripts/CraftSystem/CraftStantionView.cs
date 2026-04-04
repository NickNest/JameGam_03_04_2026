using System;
using System.Collections.Generic;
using _Project.Code.Scripts.Configs;
using _Project.Code.Scripts.Data;
using _Project.Code.Scripts.Data.TaskData;
using _Project.Code.Scripts.TaskSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Scripts.CraftSystem
{
    public class CraftStantionView : MonoBehaviour, IManualUpdate
    {
        [SerializeField] private ResourceView _resourceSlotPrefab;
        [SerializeField] private Transform _resourceSlotsContainer;
        [SerializeField] private Button _craftButton;
        [SerializeField] private Image _taskIcon;
        [SerializeField] private RectTransform _animRectTransform;


        [Header("Shake Settings")]
        [SerializeField] private float _shakeDuration = 0.3f;
        [SerializeField] private float _shakeStrength = 0.2f;

        private ITaskService _taskService;
        private ResourceIconConfig _resourceIconConfig;
        private TaskIconConfig _taskIconConfig;
        private readonly List<ResourceView> _spawnedSlots = new();
        private readonly List<(ResourceType type, int cost)> _activeCosts = new();

        public void ManualAwake(ITaskService taskService, ResourceIconConfig resourceIconConfig, TaskIconConfig taskIconConfig)
        {
            _taskService = taskService;
            _resourceIconConfig = resourceIconConfig;
            _taskIconConfig = taskIconConfig;

            _taskService.OnTaskStarted += OnTaskStarted;
            _craftButton.onClick.AddListener(OnCraftClicked);

            if (_taskService.HasActiveTask)
                OnTaskStarted(_taskService.CurrentTask.Value);
        }

        public void ManualUpdate(float deltaTime)
        {
            if (_activeCosts.Count > 0)
                RefreshSlots();
        }

        private void OnDestroy()
        {
            if (_taskService != null)
                _taskService.OnTaskStarted -= OnTaskStarted;

            _craftButton.onClick.RemoveListener(OnCraftClicked);
            _shakeTween?.Kill();
        }

        private void OnTaskStarted(TaskData task)
        {
            _activeCosts.Clear();

            var cost = task.CostInfo;
            if (cost.CrystalCost > 0) _activeCosts.Add((ResourceType.Crystal, cost.CrystalCost));
            if (cost.PolymerCost > 0) _activeCosts.Add((ResourceType.Polymer, cost.PolymerCost));
            if (cost.NanoGelCost > 0) _activeCosts.Add((ResourceType.NanoGel, cost.NanoGelCost));

            RebuildSlots();
            RefreshSlots();
            RefreshIcon(task.ResultType);
        }

        private void RefreshIcon(TaskResultType resultType)
        {
            var icon = _taskIconConfig.GetIcon(resultType);
            _taskIcon.sprite = icon;
            _taskIcon.enabled = icon != null;
        }

        private void RebuildSlots()
        {
            foreach (var slot in _spawnedSlots)
                Destroy(slot.gameObject);
            _spawnedSlots.Clear();

            foreach (var (type, cost) in _activeCosts)
            {
                var slot = Instantiate(_resourceSlotPrefab, _resourceSlotsContainer);
                slot.SetData(type, 0, cost, _resourceIconConfig);
                _spawnedSlots.Add(slot);
            }
        }

        private void RefreshSlots()
        {
            var resources = GameData.Instance.Resources;

            for (var i = 0; i < _activeCosts.Count && i < _spawnedSlots.Count; i++)
            {
                var (type, required) = _activeCosts[i];
                var available = resources[type];
                var slot = _spawnedSlots[i];

                slot.SetData(type, available, required, _resourceIconConfig);
            }
        }

        private void OnCraftClicked()
        {
            if (!_taskService.HasActiveTask)
                return;

            var task = _taskService.CurrentTask.Value;

            if (CanCraft(task.CostInfo))
            {
                SpendResources(task.CostInfo);
                GameData.Instance.AddResource(ResourceType.Credit, task.CreditReward);
                ClearAll();
                _taskService.CompleteCurrentTask();
            }
            else
            {
                Debug.Log("Not enough resources to craft!");
                Shake();
            }
        }

        private void ClearAll()
        {
            foreach (var slot in _spawnedSlots)
                Destroy(slot.gameObject);

            _spawnedSlots.Clear();
            _activeCosts.Clear();
            _taskIcon.sprite = null;
            _taskIcon.enabled = false;
        }

        private bool CanCraft(ProductionCost cost)
        {
            var resources = GameData.Instance.Resources;

            if (cost.CrystalCost > 0 && resources[ResourceType.Crystal] < cost.CrystalCost)
                return false;
            if (cost.PolymerCost > 0 && resources[ResourceType.Polymer] < cost.PolymerCost)
                return false;
            if (cost.NanoGelCost > 0 && resources[ResourceType.NanoGel] < cost.NanoGelCost)
                return false;

            return true;
        }

        private void SpendResources(ProductionCost cost)
        {
            if (cost.CrystalCost > 0)
                GameData.Instance.AddResource(ResourceType.Crystal, -cost.CrystalCost);
            if (cost.PolymerCost > 0)
                GameData.Instance.AddResource(ResourceType.Polymer, -cost.PolymerCost);
            if (cost.NanoGelCost > 0)
                GameData.Instance.AddResource(ResourceType.NanoGel, -cost.NanoGelCost);
        }

        private Tweener _shakeTween;

        private void Shake()
        {
            _shakeTween?.Kill();
            _craftButton.interactable = false;
            _animRectTransform.anchoredPosition = Vector2.zero;
            _shakeTween = _animRectTransform
                .DOShakeAnchorPos(_shakeDuration, new Vector2(_shakeStrength, 0), vibrato: 3, randomness: 0, fadeOut: true)
                .SetEase(Ease.OutSine)
                .SetLink(gameObject)
                .OnKill(() => _craftButton.interactable = true);
        }
    }
}
