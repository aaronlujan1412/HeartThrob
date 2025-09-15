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
        var entities = world.Query<TransformComponent, SpriteComponent>();

        foreach (int entity in entities)
        {
            var transform = world.GetComponent<TransformComponent>(entity);
            var sprite = world.GetComponent<SpriteComponent>(entity);
            
            spriteBatch.Draw(
                sprite.Texture,
                transform.Position,
                sprite.Color);
        }
    }

    public void RenderAll(World world, SpriteBatch spriteBatch)
    {
        var entities = world.Query<TransformComponent, SpriteComponent>();
    }
}