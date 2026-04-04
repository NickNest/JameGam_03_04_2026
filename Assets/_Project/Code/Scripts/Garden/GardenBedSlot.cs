using System.Linq;
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
        [SerializeField] private Plant[] _plantPrefabs;
        
        private GameConfig _config;
        private ITimerService _timer;
        private Plant _plantInstance;
        private bool _isOccupied;
        
        public void Initialize(GameConfig config, ITimerService timer)
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
                        Destroy(_plantInstance);
                    }
                }
            }
            else
            {
                Debug.Log($"GardenBedSlot with id {_id} has not been occupied");
            }
        }

        private void OnPlantChosen(PlantType plantType)
        {
            var plant = _plantPrefabs.FirstOrDefault(plant => plant.Type == plantType);
            _plantInstance = Instantiate(plant, _plantParent);
            _plantInstance.Initialize(_config, _timer);
        }
        
        private int GetDefaultProductivity(ResourceType resourceType) => 
            _config.GardenConfig.GetGrowableResourceData(resourceType).DefaultProductionProductivity;
    }
}