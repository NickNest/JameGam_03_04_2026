using System.Collections.Generic;
using _Project.Code.Scripts.Data;
using _Project.Code.Scripts.GameController;
using _Project.Code.Scripts.InputResolverService;
using _Project.Code.Scripts.TimerService;
using UnityEngine;

namespace _Project.Code.Scripts.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private InputResolver _inputResolver;
        [SerializeField] private GameController.GameController _gameController;
        [SerializeField] private GameConfig _gameConfig;
        private GameData _gameData;
        private ITimerService _timerService;

        private void Awake() {
            InitConfig();
            _timerService = new TimerService.TimerService();
            
            var manualUpdates = new List<IManualUpdate> {   _inputResolver, 
                                                            _timerService as IManualUpdate };

            _gameController.ManualAwake(manualUpdates);
            _inputResolver.ManualAwake();
        }

        private void Start() {
            
        }

        
        private void InitConfig() {
            _gameData = new GameData(_gameConfig);

            _gameData.Initialize();
        }
    }
}