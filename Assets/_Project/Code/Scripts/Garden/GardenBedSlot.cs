using System.Linq;
using _Project.Code.Scripts.Configs;
using _Project.Code.Scripts.Data;
using _Project.Code.Scripts.Timer;
using _Project.Code.Scripts.UIService;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Code.Scripts.Garden
{
    public class GardenBedSlot: MonoBehaviour
    {
        [SerializeField] private Transform _plantParent;
        [SerializeField] private Transform _panelPosition;
        [SerializeField] private Plant[] _plantPrefabs;
        
        private GameConfig _config;
        private Transform _canvasParent;
        private ITimerService _timer;
        private Plant _plantInstance;
        private IPanelShower _panelShower;
        private bool _isOccupied;
        
        private BasePanel _panel;
        
        public void Initialize(
            IPanelShower panelShower,
            GameConfig config, 
            ITimerService timer,
            Transform canvasParent)
        {
            _panelShower = panelShower;
            _config = config;
            _timer = timer;
            _canvasParent = canvasParent;
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
                        Destroy(_plantInstance.gameObject);
                        _isOccupied = false;
                    }
                }
            }
            else
            {
                var settings = new PlantChoosePanelSettings()
                {
                    Callback = OnPlantChosen,
                    Position = _panelPosition.position,
                };
                _panelShower.ShowView(PanelType.PlantPanelInfo, settings, _canvasParent);
            }
        }

        private void OnPlantChosen(PlantType plantType)
        {
            var plant = _plantPrefabs.FirstOrDefault(plant => plant.Type == plantType);
            _plantInstance = Instantiate(plant, _plantParent);
            _plantInstance.Initialize(_config, _timer);
            _isOccupied = true;
            _panelShower.HideView(PanelType.PlantPanelInfo);
        }
        
        private int GetDefaultProductivity(ResourceType resourceType) => 
            _config.GardenConfig.GetGrowableResourceData(resourceType).DefaultProductivity;
    }
}