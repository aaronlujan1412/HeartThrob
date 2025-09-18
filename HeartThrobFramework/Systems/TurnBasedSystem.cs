using HeartThrobFramework.Core;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Systems;

public class TurnBasedSystem : ISystem
{
    public World World { get; set; }
    public void Update(float deltaTime)
    {
        return;
//        var entities = world.Query<ActionOrder>();
    }

    public void Render(SpriteBatch spriteBatch)
    {
        return;
    }
}