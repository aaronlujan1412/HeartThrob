using HeartThrobFramework.Core;
using HeartThrobFramework.Components;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Systems;

public class MovementSystem : ISystem
{
    public World World { get; set; }
    public void Update(float deltaTime)
    {
        var entities = World.Query<TransformComponent, VelocityComponent>();

        foreach (var entity in entities)
        {
            var transform = World.GetComponent<TransformComponent>(entity);;
            var velocity = World.GetComponent<VelocityComponent>(entity);

            transform.Position += (velocity.Value * deltaTime);
            
            World.UpdateComponent<TransformComponent>(entity, transform);;
        }
    }

    public void Render(SpriteBatch spriteBatch)
    {
        return;
    }
}