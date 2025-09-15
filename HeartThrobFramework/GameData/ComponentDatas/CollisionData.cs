using MonoGame.Extended;

namespace HeartThrobFramework.GameData.ComponentDatas
{
    public record struct CollisionData
    {
        public RectangleF Hitbox { get; set; }
        public bool IsColliding { get; set; }
    }
}
