using Microsoft.Xna.Framework;

namespace HeartThrobFramework.Components;

public struct CollisionComponent : IComponent
{
    public Rectangle CollisionRectangle;
    public bool IsColliding;
}