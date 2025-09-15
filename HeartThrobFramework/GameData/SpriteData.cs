using Microsoft.Xna.Framework;

namespace HeartThrobFramework.GameData;

public record struct SpriteData
{
    public string Texture { get; set; }
    public Color Color  { get; set; }
    public bool IsEquipped { get; set; }
}