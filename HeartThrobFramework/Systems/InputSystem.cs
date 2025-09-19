using HeartThrobFramework.Core;
using HeartThrobFramework.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Numerics;
using HeartThrobFramework.GameData.StateEnums;
using HeartThrobFramework.Managers;

namespace HeartThrobFramework.Systems;

public class InputSystem(InputManager inputManager) : ISystem
{
    private readonly InputManager _inputManager = inputManager;
    private const float moveSpeed = 100f;

    public World World { get; set; } = null!;
    public void Update(float deltaTime)
    {
        var entities = World.Query<PlayerControlledComponent, VelocityComponent>();

        foreach (var entity in entities)
        {
            var velocity = World.GetComponent<VelocityComponent>(entity);

            velocity.Value = Vector2.Zero;

            if (_inputManager.CurrentKeyboardState.IsKeyDown(Keys.W)) velocity.Value.Y -= moveSpeed;
            if (_inputManager.CurrentKeyboardState.IsKeyDown(Keys.A)) velocity.Value.X -= moveSpeed;
            if (_inputManager.CurrentKeyboardState.IsKeyDown(Keys.S)) velocity.Value.Y += moveSpeed;
            if (_inputManager.CurrentKeyboardState.IsKeyDown(Keys.D)) velocity.Value.X += moveSpeed;

            World.UpdateComponent(entity, velocity);
        }

        if (_inputManager.CurrentKeyboardState.IsKeyDown(Keys.Escape) && _inputManager.PreviousKeyboardState.IsKeyUp(Keys.Escape))
        {
            int commandEntity = World.CreateEntity();
            World.AddComponent<StateToggleComponent>(commandEntity, new StateToggleComponent(GameStates.TimeStopped));
        }
    }
}

