using System;
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

            GameData.Instance.Upgrades.PlantUpgradesData.TryGetValue(plantType, out var upgradeData);
            if (upgradeData != null)
            {
                _productivityText.text = $"Productivity \n {upgradeData.Productivity} p/s";
                _growTimeText.text = $"Grow Time \n {upgradeData.GrowTime} sec";
            }
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}