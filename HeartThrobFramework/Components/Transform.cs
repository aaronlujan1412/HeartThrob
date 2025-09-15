using HeartThrobFramework.Utils;
using Microsoft.Xna.Framework;

namespace HeartThrobFramework.Components;

[ComponentCapacity(500)]
public struct Transform : IComponent
{
    public Vector2 Position;
    public float Rotation;
    public float Scale;
}