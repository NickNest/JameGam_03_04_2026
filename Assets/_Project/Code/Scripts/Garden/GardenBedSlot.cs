using _Project.Code.Scripts.Configs;
using _Project.Code.Scripts.Data;
using _Project.Code.Scripts.Timer;
using UnityEngine;

namespace _Project.Code.Scripts.Garden
{
    public class GardenBedSlot: MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private Transform _plantParent;

        private GameConfig _config;
        private TimerService _timer;
        private Plant _plantInstance;
        private bool _isOccupied;
        
        public void Initialize(GameConfig config, TimerService timer)
        {
            _config = config;
            _timer = timer;
        }

        public void OnClicked()
        {
            if (_isOccupied)
            {
                if (_plantInstance != null)
                {
                    if (_plantInstance.IsGrown)
                    {
                        var resourceType = _plantInstance.Type.GetResourceType();
                        var productivityMultiplier = GameData.Instance.ProductionProductivityMultiplier;
                        GameData.Instance.AddResource(resourceType, GetDefaultProductivity(resourceType) * productivityMultiplier);
                    }
                }
            }
        }

        public int GetDefaultProductivity(ResourceType resourceType)
        {
            return _config.GardenConfig.GetGrowableResourceData(resourceType).DefaultProductionProductivity;
        }
    }
}