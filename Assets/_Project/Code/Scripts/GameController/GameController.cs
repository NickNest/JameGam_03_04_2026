using System.Collections.Generic;
using _Project.Code.Scripts;
using _Project.Code.Scripts.Data;
using UnityEngine;

public class GameController : MonoBehaviour {
    private GameData _gameData;
    private GameConfig _gameConfig;
    public GameData GameData => _gameData;
    public GameConfig GameConfig => _gameConfig;

    private List<IManualAwake> _manualAwakes = new();

    public void ManualAwake(List<IManualAwake> manualAwakes) {
        _gameConfig = new GameConfig();
        _gameData = new GameData(_gameConfig);
        _manualAwakes = manualAwakes;

        InitConfig();
    }

    public void ManualStart() {
        
    }

    public void Update()
    {
        foreach (var manualAwake in _manualAwakes)
        {
            manualAwake.ManualAwake(Time.deltaTime);
        }
    }

    private void InitConfig() {
        _gameData.Initialize();
    }
}