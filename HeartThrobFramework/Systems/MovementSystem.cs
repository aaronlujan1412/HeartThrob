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
        var entities = world.Query<Transform, Velocity>();

        foreach (var entity in entities)
        {
            var transform = world.GetComponent<Transform>(entity);;
            var velocity = world.GetComponent<Velocity>(entity);

            transform.Position += (velocity.Value * deltaTime);
            
            world.UpdateComponent<Transform>(entity, transform);;
        }
    }

    public void Render(World world, SpriteBatch spriteBatch)
    {
        return;
    }
}