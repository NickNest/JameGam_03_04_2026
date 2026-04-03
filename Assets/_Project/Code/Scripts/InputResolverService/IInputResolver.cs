using System;

namespace _Project.Code.Scripts.InputResolverService
{
    internal interface IInputResolver
    {
        event Action<InputEventData> OnPointerDown;
        event Action<InputEventData> OnPointerHeld;
        event Action<InputEventData> OnPointerUp;
    }
}