using HeartThrobFramework.Core;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Systems;

public interface ISystem
{
    public World World { get; set; }

    void Update(float deltaTime);

    void Render(SpriteBatch spriteBatch);
}