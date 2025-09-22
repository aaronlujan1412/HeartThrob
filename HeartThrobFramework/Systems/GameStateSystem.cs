using HeartThrobFramework.Components;
using HeartThrobFramework.Core.World;
using HeartThrobFramework.Factories;
using HeartThrobFramework.GameData.StateEnums;

namespace HeartThrobFramework.Systems;

public class GameStateSystem() : ISystem
{
    public World World { get; set; } = null!;

    public void Update(float deltaTime)
    {
        var commandEntity = World.Query<StateToggleComponent>();


        if (commandEntity.Any())
        {
            var command = World.GetComponent<StateToggleComponent>(commandEntity.First());

            switch (World.CurrentState)
            {
                case GameStates.TimeStopped:
                    if (command.State == GameStates.TimeAdvancing)
                    {
                        World.SetGameState(GameStates.TimeAdvancing);
                        World.DestroyEntity(commandEntity.First());
                    } else if (command.State == GameStates.TimeStopped)
                    {
                        World.SetGameState(GameStates.TimeAdvancing);
                        World.DestroyEntity(commandEntity.First());
                    }
                        break;

                case GameStates.TimeAdvancing:
                    if (command.State == GameStates.TimeStopped)
                    {
                        World.SetGameState(GameStates.TimeStopped);
                        World.DestroyEntity(commandEntity.First());
                    }
                    break;

                default:
                    break;
            }
        }
    }
}