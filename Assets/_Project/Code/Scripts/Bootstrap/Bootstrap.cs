using System.Collections.Generic;
using _Project.Code.Scripts.Data;
using _Project.Code.Scripts.GameController;
using _Project.Code.Scripts.InputResolverService;
using UnityEngine;

namespace _Project.Code.Scripts.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private InputResolver _inputResolver;
        [SerializeField] private Scripts.GameController.GameController _gameController;
        [SerializeField] private GameConfig _gameConfig;
        private GameData _gameData;

        private void Awake() {
            InitConfig();
            var manualUpdates = new List<IManualAwake> { _inputResolver };

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