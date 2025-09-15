using HeartThrobFramework.Core;
using HeartThrobFramework.Components;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Systems;

public class RenderSystem : ISystem
{
    public void Update(World world, float deltaTime)
    {
        return;
    }

    public void Render(World world, SpriteBatch spriteBatch)
    {
        var entities = world.Query<Transform, Sprite>();

        foreach (int entity in entities)
        {
            var transform = world.GetComponent<Transform>(entity);
            var sprite = world.GetComponent<Sprite>(entity);
            
            spriteBatch.Draw(
                sprite.Texture,
                transform.Position,
                sprite.Color);
        }
    }

    public void RenderAll(World world, SpriteBatch spriteBatch)
    {
        var entities = world.Query<Transform, Sprite>();
    }
}