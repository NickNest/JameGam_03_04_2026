using _Project.Code.Scripts.InputResolverService;
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
        private IInputResolver _inputResolver;
        
        public void Initialize(GameConfig config, TimerService timer)
        {
            _config = config;
            _timer = timer;
        }
    }
}