using MonoGame.Extended;

namespace HeartThrobFramework.Components;

public struct CollisionComponent : IComponent
{
    public RectangleF CollisionRectangle;
    public bool IsColliding;
}