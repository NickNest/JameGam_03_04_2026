using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

internal class InputResolver : MonoBehaviour, IInputResolver
{
    public event Action<InputEventData> OnPointerDown;
    public event Action<InputEventData> OnPointerHeld;
    public event Action<InputEventData> OnPointerUp;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _worldRaycastDistance = 1000f;
    [SerializeField] private LayerMask _worldLayerMask = ~0;

    private readonly List<RaycastResult> _uiRaycastResults = new();
    private static readonly int[] ButtonIndices = { 0, 1, 2 };

    private void Update()
    {
        for (int i = 0; i < ButtonIndices.Length; i++)
        {
            int index = ButtonIndices[i];
            var button = (MouseButton)index;

            if (Input.GetMouseButtonDown(index))
            {
                OnPointerDown?.Invoke(BuildEventData(button));
            }
            else if (Input.GetMouseButton(index))
            {
                OnPointerHeld?.Invoke(BuildEventData(button));
            }

            if (Input.GetMouseButtonUp(index))
            {
                OnPointerUp?.Invoke(BuildEventData(button));
            }
        }
    }

    private InputEventData BuildEventData(MouseButton button)
    {
        Vector2 screenPos = Input.mousePosition;

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
