using HeartThrobFramework.Components;
using HeartThrobFramework.Core;
using HeartThrobFramework.GameData.StateEnums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeartThrobFramework.Systems;

public class GameStateSystem : ISystem
{
    public World World { get; set; }


    public event Action<GameStates> OnGameStateChanged;
    public void Update(World world, float deltaTime)
    {
        var worldEntity = world.Query<GameStateComponent>();

        foreach (var entity in worldEntity)
        {
            {
                if (world.CurrentState == GameStates.TimeStopped)
                {
                    world.SetGameState(GameStates.TimeAdvancing);
                    return;
                }
                world.SetGameState(GameStates.TimeStopped);
            }
        }
    }

    public void Render(World world, SpriteBatch spriteBatch)
    {
        return;
    }

    public void ChangeGameState(World world, GameStates state, int worldEntity)
    {
        world.UpdateComponent<GameStateComponent>(worldEntity, new GameStateComponent(state));
        return;
    }
}