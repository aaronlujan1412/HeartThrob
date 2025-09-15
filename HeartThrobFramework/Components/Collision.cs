using Microsoft.Xna.Framework;

namespace HeartThrobFramework.Components;

public struct Collision : IComponent
{
    public Rectangle CollisionRectangle;
    public bool IsColliding;
}