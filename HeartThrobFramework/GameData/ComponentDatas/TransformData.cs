using Microsoft.Xna.Framework;

namespace HeartThrobFramework.GameData.ComponentDatas;

public record struct TransformData
{
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public float Scale { get; set; }
}
