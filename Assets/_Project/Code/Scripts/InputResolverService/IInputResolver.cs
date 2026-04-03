using System;

internal interface IInputResolver
{
    event Action<InputEventData> OnPointerDown;
    event Action<InputEventData> OnPointerHeld;
    event Action<InputEventData> OnPointerUp;
}