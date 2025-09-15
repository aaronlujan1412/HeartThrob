using HeartThrobFramework.Core;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Systems;

public class TurnBasedSystem : ISystem
{
    public void Update(World world, float deltaTime)
    {
        return;
//        var entities = world.Query<ActionOrder>();
    }

    public void Render(World world, SpriteBatch spriteBatch)
    {
        return;
    }
}