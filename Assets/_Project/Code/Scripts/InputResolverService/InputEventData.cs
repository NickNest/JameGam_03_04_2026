using System;
using UnityEngine;

internal enum MouseButton
{
    Left = 0,
    Right = 1,
    Middle = 2
}

internal enum InputTarget
{
    None,
    Canvas,
    World
}

internal readonly struct InputEventData
{
    public readonly MouseButton Button;
    public readonly InputTarget Target;
    public readonly Vector2 ScreenPosition;
    public readonly GameObject HitObject;
    public readonly RaycastHit? WorldHit;

    public InputEventData(MouseButton button, InputTarget target, Vector2 screenPosition,
        GameObject hitObject, RaycastHit? worldHit)
    {
        Button = button;
        Target = target;
        ScreenPosition = screenPosition;
        HitObject = hitObject;
        WorldHit = worldHit;
    }

    public bool IsCanvasHit => Target == InputTarget.Canvas;
    public bool IsWorldHit => Target == InputTarget.World;
}
