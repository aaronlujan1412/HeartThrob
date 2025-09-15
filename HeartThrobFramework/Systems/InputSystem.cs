using HeartThrobFramework.Core;
using HeartThrobFramework.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeartThrobFramework.Systems;

public class InputSystem : ISystem
{
    private const float moveSpeed = 100f;
    public void Update(World world, float deltaTime)
    {
        var entities = world.Query<PlayerControlledComponent, VelocityComponent>();
        var keyboardState = Keyboard.GetState();
        
        foreach (var entity in entities)
        {
            var velocity = world.GetComponent<VelocityComponent>(entity);

            velocity.Value.X = 0;
            velocity.Value.Y = 0;
            
            if (keyboardState.IsKeyDown(Keys.A)) velocity.Value.X -= moveSpeed;

            if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.Value.X = -100f;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity.Value.X = 100f;
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                velocity.Value.Y = -100f;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                velocity.Value.Y = 100f;
            }
            
            
            world.UpdateComponent(entity, velocity);
        }
    }

    public void Render(World world, SpriteBatch spriteBatch)
    {
        return;
    }
}