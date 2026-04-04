using System;
using _Project.Code.Scripts.Configs;
using _Project.Code.Scripts.Data;
using _Project.Code.Scripts.Garden;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Scripts.UI
{
    public class PlantChooseView: MonoBehaviour
    {
        [SerializeField] private PlantType plantType;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _productivityText;
        [SerializeField] private TMP_Text _growTimeText;
        
        public PlantType PlantType => plantType;

        public void Initialize(Action<PlantType> action)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => action(plantType));

            var produceMultiplier = GameData.Instance.ProduceMultiplier;
            var growSpeedMultiplier = GameData.Instance.GrowSpeedMultiplier;
            var config = GameData.Instance.GetConfig();
            var resourceData = config.GardenConfig.GetGrowableResourceData(plantType.GetResourceType());
            var resultProduce = resourceData.DefaultProductivity * produceMultiplier;
            var resultGrowTime = resourceData.GrowthTime / growSpeedMultiplier;
            
            _productivityText.text = $"Productivity \n {resultProduce} p/s";
            _growTimeText.text = $"Grow Time \n {resultGrowTime} sec";
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}