using HeartThrobFramework.Utils;
using Microsoft.Xna.Framework;

namespace HeartThrobFramework.Components;

[ComponentCapacity(500)]
public struct TransformComponent : IComponent
{
    public Vector2 Position;
    public float Rotation;
    public float Scale;
}