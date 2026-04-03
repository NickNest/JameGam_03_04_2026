using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private InputResolver _inputResolver;
    [SerializeField] private GameController _gameController;

    private void Awake() {
        var manualUpdates = new List<IManualAwake> { _inputResolver };
        
        _gameController.ManualAwake(manualUpdates);
        _inputResolver.ManualAwake();
    }

    private void Start() {
        
    }
}