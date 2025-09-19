using HeartThrobFramework.Components;
using Microsoft.Xna.Framework.Graphics;
using HeartThrobFramework.Core.World;

namespace HeartThrobFramework.Systems;

public class RenderSystem(SpriteBatch spriteBatch) : ISystem
{
    public World World { get; set; } = null!;

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
}