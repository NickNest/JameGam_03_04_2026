using System.Collections.Generic;
using _Project.Code.Scripts.Data;
using _Project.Code.Scripts.Game;
using _Project.Code.Scripts.InputResolverService;
using _Project.Code.Scripts.UIService;
using _Project.Code.Scripts.Timer;
using UnityEngine;

namespace _Project.Code.Scripts.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private InputResolver _inputResolver;
        [SerializeField] private UIController _uiManager;
        [SerializeField] private GameController _gameController;
        [SerializeField] private GameConfig _gameConfig;
        private GameData _gameData;
        private ITimerService _timerService;

        private void Awake() {
            InitConfig();
            _timerService = new TimerService();
            
            var manualUpdates = new List<IManualUpdate> {   _inputResolver, 
                                                            _timerService as IManualUpdate };

            _inputResolver.ManualAwake();
            _uiManager.Initialize(_inputResolver);
            _gameController.ManualAwake(manualUpdates);
        }

        private void Start() {
            
        }
        
        private void InitConfig() {
            _gameData = new GameData(_gameConfig);

            _gameData.Initialize();
        }
    }
}