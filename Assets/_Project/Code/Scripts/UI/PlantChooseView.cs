using System;
using _Project.Code.Scripts.Garden;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Scripts.UI
{
    public class PlantChooseView: MonoBehaviour
    {
        [SerializeField] private PlantType plantType;
        [SerializeField] private Button _button;
        
        public PlantType PlantType => plantType;

        public void Initialize(Action<PlantType> action)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => action(plantType));
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}