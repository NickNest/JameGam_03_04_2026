using System;
using _Project.Code.Scripts.InputResolverService;
using _Project.Code.Scripts.Timer;
using UnityEngine;

namespace _Project.Code.Scripts.Garden
{
    public class GardenBed: MonoBehaviour
    {
        [SerializeField] private GardenBedSlot[] _slots;
        
        private IInputResolver _inputResolver;
        private ITimerService _timerService;
        private GameConfig _gameConfig;

        public void Initialize(GameConfig gameConfig, IInputResolver inputResolver, ITimerService timerService)
        {
            _inputResolver = inputResolver;
            _gameConfig = gameConfig;
            _timerService = timerService;
            
            _inputResolver.OnPointerDown += OnPointerDown;
            
            foreach (var slot in _slots)
            {
                slot.Initialize(_gameConfig, _timerService);
            }
        }

        private void OnPointerDown(InputEventData inputData)
        {
            if (inputData.HitObject.TryGetComponent<GardenBedSlot>(out var gardenBedSlot))
            {
                gardenBedSlot.OnClicked();
            }
        }

        private void OnDestroy()
        {
            _inputResolver.OnPointerDown -= OnPointerDown;
        }
    }
}