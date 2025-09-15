using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Components;

public struct SpriteComponent : IComponent
{
    public Texture2D Texture;
    public Color Color;
    public bool IsEquipped;
}