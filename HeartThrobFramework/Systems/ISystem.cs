using HeartThrobFramework.Core;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Systems;

public interface ISystem
{
    void Update(World world, float deltaTime);

    void Render(World world, SpriteBatch spriteBatch);
}