using HeartThrobFramework.Core;
using HeartThrobFramework.Components;

using System.Data;
using System.Threading.Tasks.Dataflow;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Systems;

public class MovementSystem : ISystem
{
    public void Update(World world, float deltaTime)
    {
        var entities = world.Query<TransformComponent, VelocityComponent>();

        foreach (var entity in entities)
        {
            var transform = world.GetComponent<TransformComponent>(entity);;
            var velocity = world.GetComponent<VelocityComponent>(entity);

            transform.Position += (velocity.Value * deltaTime);
            
            world.UpdateComponent<TransformComponent>(entity, transform);;
        }
    }

    public void Render(World world, SpriteBatch spriteBatch)
    {
        return;
    }
}