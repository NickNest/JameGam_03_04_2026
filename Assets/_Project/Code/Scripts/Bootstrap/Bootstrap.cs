using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private InputResolver _inputResolver;

    private void Awake() {
        _inputResolver.ManualAwake();
    }

    private void Start() {
        
    }
}