using _Project.Code.Scripts.InputResolverService;
using _Project.Code.Scripts.Timer;
using _Project.Code.Scripts.UI;
using _Project.Code.Scripts.UIService;
using UnityEngine;

namespace _Project.Code.Scripts.Garden
{
    public class GardenBed: MonoBehaviour
    {
        [SerializeField] private Transform _canvasParent;
        [SerializeField] private GardenBedSlot[] _slots;
        
        private IPanelShower _panelShower;
        private IInputResolver _inputResolver;
        private ITimerService _timerService;
        private GameConfig _gameConfig;

        public void Initialize(IPanelShower panelShower, GameConfig gameConfig, IInputResolver inputResolver, ITimerService timerService)
        {
            _panelShower = panelShower;
            _inputResolver = inputResolver;
            _gameConfig = gameConfig;
            _timerService = timerService;
            
            _inputResolver.OnPointerDown += OnPointerDown;
            
            foreach (var slot in _slots)
            {
                slot.Initialize(_panelShower, _gameConfig, _timerService, _canvasParent);
            }
        }

        private void OnPointerDown(InputEventData inputData)
        {
            if (inputData.Target == InputTarget.Canvas)
            {
                if (inputData.HitObject != null)
                {
                    if (!inputData.HitObject.TryGetComponent<PlantChooseView>(out var view))
                    {
                        _panelShower.HideView(PanelType.PlantPanelInfo);
                    }
                }
            }
            else
            {
                _panelShower.HideView(PanelType.PlantPanelInfo);
            }
            
            if (inputData.Target != InputTarget.World) return;

            if (inputData.HitObject == null) return;
            
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