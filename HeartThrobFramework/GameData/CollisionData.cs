using MonoGame.Extended;

namespace HeartThrobFramework.GameData
{
    public record struct CollisionData
    {
        public RectangleF Hitbox { get; set; }
        public bool IsColliding { get; set; }
    }
}
