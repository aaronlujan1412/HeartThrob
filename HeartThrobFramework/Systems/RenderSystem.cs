using HeartThrobFramework.Core;
using HeartThrobFramework.Components;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Systems;

public class RenderSystem(SpriteBatch spriteBatch) : ISystem
{
    public required World World { get; set; }

    private readonly SpriteBatch _spriteBatch = spriteBatch;

    public void Update(float deltaTime)
    {
        return;
    }

    public void Render(SpriteBatch spriteBatch)
    {
        var entities = World.Query<TransformComponent, SpriteComponent>();

        foreach (int entity in entities)
        {
            var transform = World.GetComponent<TransformComponent>(entity);
            var sprite = World.GetComponent<SpriteComponent>(entity);
            
            spriteBatch.Draw(
                sprite.Texture,
                transform.Position,
                sprite.Color);
        }
    }

    public void RenderEntity(SpriteBatch spriteBatch, int entity)
    {
        World.


        world.AddComponent<TransformComponent>(entity)

        var transform = world.GetComponent<TransformComponent>(entity);
        var sprite = world.GetComponent<SpriteComponent>(entity);

        spriteBatch.Draw(
            sprite.Texture,
            transform.Position,
            sprite.Color);
    }

    public void RenderAll(World world, SpriteBatch spriteBatch)
    {
        var entities = world.Query<TransformComponent, SpriteComponent>();
    }
}