using System.Collections.Generic;
using _Project.Code.Scripts.Data;
using _Project.Code.Scripts.Game;
using _Project.Code.Scripts.InputResolverService;
using _Project.Code.Scripts.UIService;
using _Project.Code.Scripts.Configs;
using _Project.Code.Scripts.Garden;
using _Project.Code.Scripts.TaskSystem;
using _Project.Code.Scripts.Timer;
using UnityEngine;

namespace _Project.Code.Scripts.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private InputResolver _inputResolver;
        [SerializeField] private UIController _uiManager;
        [SerializeField] private GameController _gameController;
        [SerializeField] private TaskSystemView _taskSystemView;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private TaskConfig _taskConfig;
        [SerializeField] private GardenBed _gardenBed;
        private GameData _gameData;
        private ITimerService _timerService;
        private ITaskService _taskService;

        private void Awake() {
            InitConfig();
            _timerService = new TimerService();
            
            var manualUpdates = new List<IManualUpdate> {   _inputResolver, 
                                                            _timerService as IManualUpdate };

            _inputResolver.ManualAwake();
            _uiManager.Initialize(_inputResolver);
            _taskService = new TaskService(_taskConfig.Tasks);
            _taskSystemView.Initialize(_taskService);
            _gameController.ManualAwake(manualUpdates);
        }

        private void Start() {
            
        }
        
        private void InitConfig() {
            _gameData = new GameData(_gameConfig);

            _gameData.Initialize();
            
            _gardenBed.Initialize(_gameConfig, _inputResolver, _timerService);
        }
    }
}