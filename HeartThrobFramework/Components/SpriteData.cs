using Microsoft.Xna.Framework;

namespace HeartThrobFramework.Components;

public struct SpriteData
{
    public string Texture { get; set; }
    public Color Color  { get; set; }
    public bool IsEquipped { get; set; }
}