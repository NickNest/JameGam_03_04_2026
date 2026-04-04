using _Project.Code.Scripts.Timer;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Scripts.Garden
{
    public class Plant: MonoBehaviour, IManualUpdate
    {
        private GameConfig _config;
        private TimerService _timerService;

        private TimerHandle _timer;
        private bool _isOnHalfGrown;

        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private Image _image;

        public PlantType Type;

        public bool IsGrown { get; private set; }


        public void Initialize(GameConfig config, TimerService timerService)
        {
            _timerService = timerService;
            _config = config;

            _image.sprite = _sprites[0];
            
            _timer = Type switch
            {
                PlantType.Crystal => _timerService.Start(_config.CrystalGrowthTime, OnGrown),
                PlantType.NanoGel => _timerService.Start(_config.NanoGelGrowthTime, OnGrown),
                PlantType.Polymer => _timerService.Start(_config.PolymerGrowthTime, OnGrown),
                _ => _timer
            };
        }

        public void ManualUpdate(float deltaTime)
        {
            if (_isOnHalfGrown)
            {
                return;
            }
            
            var timeRemaining = _timerService.GetRemaining(_timer);
            var castedTimeRemaining = timeRemaining / 2;
            if (timeRemaining >= castedTimeRemaining)
            {
                _isOnHalfGrown = true;
                _image.sprite = _sprites[1];
            }
        }

        private void OnGrown()
        {
            IsGrown = true;
            _image.sprite = _sprites[2];
        }
    }
}