using HeartThrobFramework.Core;
using HeartThrobFramework.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Numerics;
using HeartThrobFramework.GameData.StateEnums;

namespace HeartThrobFramework.Systems;

public class InputSystem : ISystem
{
    public World World { get; set; }
    private KeyboardState _lastKeyboardState;

    private const float moveSpeed = 100f;
    public void Update(float deltaTime)
    {
        var keyboardState = Keyboard.GetState();

        var entities = World.Query<PlayerControlledComponent, VelocityComponent>();

        foreach (var entity in entities)
        {
            var velocity = World.GetComponent<VelocityComponent>(entity);

            velocity.Value = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.W)) velocity.Value.Y -= moveSpeed;
            if (keyboardState.IsKeyDown(Keys.A)) velocity.Value.X -= moveSpeed;
            if (keyboardState.IsKeyDown(Keys.S)) velocity.Value.Y += moveSpeed;
            if (keyboardState.IsKeyDown(Keys.D)) velocity.Value.X += moveSpeed;

            //if (keyboardState.IsKeyDown(Keys.A))
            //{
            //    velocity.Value.X = -100f;
            //}

            //if (keyboardState.IsKeyDown(Keys.D))
            //{
            //    velocity.Value.X = 100f;
            //}

            //if (keyboardState.IsKeyDown(Keys.W))
            //{
            //    velocity.Value.Y = -100f;
            //}

            //if (keyboardState.IsKeyDown(Keys.S))
            //{
            //    velocity.Value.Y = 100f;
            //}




            //if (keyboardState.IsKeyDown(Keys.Escape) && 
            //    _lastKeyboardState.IsKeyUp(Keys.Escape))
            //{
            //    OnMenuButtonPressed?.Invoke("esc");
            //}

            World.UpdateComponent(entity, velocity);
        }

        if (keyboardState.IsKeyDown(Keys.Escape) && _lastKeyboardState.IsKeyUp(Keys.Escape))
        {
            int commandEntity = World.CreateEntity();
            World.AddComponent<StateToggleComponent>(commandEntity, new StateToggleComponent(GameStates.TimeStopped));
        }

        _lastKeyboardState = keyboardState;
    }

    public void Render(SpriteBatch spriteBatch) { }
}

