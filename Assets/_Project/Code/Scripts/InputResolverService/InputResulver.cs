using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

internal class InputResolver : MonoBehaviour, IInputResolver
{
    public event Action<InputEventData> OnPointerDown;
    public event Action<InputEventData> OnPointerHeld;
    public event Action<InputEventData> OnPointerUp;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _worldRaycastDistance = 1000f;
    [SerializeField] private LayerMask _worldLayerMask = ~0;

    private readonly List<RaycastResult> _uiRaycastResults = new();

    private void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;

        ProcessButton(mouse.leftButton, MouseButton.Left);
        ProcessButton(mouse.rightButton, MouseButton.Right);
        ProcessButton(mouse.middleButton, MouseButton.Middle);
    }

    private void ProcessButton(UnityEngine.InputSystem.Controls.ButtonControl control, MouseButton button)
    {
        if (control.wasPressedThisFrame)
        {
            OnPointerDown?.Invoke(BuildEventData(button));
        }
        else if (control.isPressed)
        {
            OnPointerHeld?.Invoke(BuildEventData(button));
        }

        if (control.wasReleasedThisFrame)
        {
            OnPointerUp?.Invoke(BuildEventData(button));
        }
    }

    private InputEventData BuildEventData(MouseButton button)
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        if (IsPointerOverUI(screenPos, out GameObject uiElement))
            return new InputEventData(button, InputTarget.Canvas, screenPos, uiElement, null);

        if (TryWorldRaycast(screenPos, out RaycastHit hit))
            return new InputEventData(button, InputTarget.World, screenPos, hit.collider.gameObject, hit);

        return new InputEventData(button, InputTarget.None, screenPos, null, null);
    }

    private bool IsPointerOverUI(Vector2 screenPos, out GameObject uiElement)
    {
        uiElement = null;

        if (EventSystem.current == null || !EventSystem.current.IsPointerOverGameObject())
            return false;

        var pointerData = new PointerEventData(EventSystem.current) { position = screenPos };

        _uiRaycastResults.Clear();
        EventSystem.current.RaycastAll(pointerData, _uiRaycastResults);

        if (_uiRaycastResults.Count > 0)
        {
            uiElement = _uiRaycastResults[0].gameObject;
            return true;
        }

        return false;
    }

    private bool TryWorldRaycast(Vector2 screenPos, out RaycastHit hit)
    {
        Camera cam = _camera ? _camera : Camera.main;

        if (cam == null)
        {
            hit = default;
            return false;
        }

        Ray ray = cam.ScreenPointToRay(screenPos);
        return Physics.Raycast(ray, out hit, _worldRaycastDistance, _worldLayerMask);
    }
}
