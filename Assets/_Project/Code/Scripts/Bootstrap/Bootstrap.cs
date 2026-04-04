using System.Collections.Generic;
using _Project.Code.Scripts.Data;
using _Project.Code.Scripts.Game;
using _Project.Code.Scripts.InputResolverService;
using _Project.Code.Scripts.UIService;
using _Project.Code.Scripts.TaskSystem;
using _Project.Code.Scripts.Timer;
using _Project.Code.Scripts.CraftSystem;
using UnityEngine;

namespace _Project.Code.Scripts.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private InputResolver _inputResolver;
        [SerializeField] private UIController _uiManager;
        [SerializeField] private GameController _gameController;
        [SerializeField] private TaskSystemView _taskSystemView;
        [SerializeField] private CraftStantionView _craftStantionView;
        [SerializeField] private GameConfig _gameConfig;
        private GameData _gameData;
        private ITimerService _timerService;
        private ITaskService _taskService;

        private void Awake() {
            InitConfig();
            _timerService = new TimerService();
            
            var manualUpdates = new List<IManualUpdate> {   _inputResolver, 
                                                            _timerService as IManualUpdate,
                                                            _craftStantionView,};

            _inputResolver.ManualAwake();
            _uiManager.Initialize(_inputResolver);
            _taskService = new TaskService(_gameConfig.TaskConfig.Tasks);
            _taskSystemView.ManualAwake(_taskService, _gameConfig.TaskIconConfig);
            _craftStantionView.ManualAwake(_taskService, _gameConfig.ResourceIconConfig, _gameConfig.TaskIconConfig);
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