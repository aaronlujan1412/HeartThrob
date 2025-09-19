using HeartThrobFramework.Components;
using HeartThrobFramework.Core;
using HeartThrobFramework.Factories;
using HeartThrobFramework.GameData.StateEnums;
using HeartThrobFramework.GameData.Template;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Systems;

public class GameStateSystem(EntityFactory ef) : ISystem
{
    public World World { get; set; }
    private int _stateEntity = -1;

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
                        World.DestroyEntity(_stateEntity);
                        World.DestroyEntity(commandEntity.First());
                    } else if (command.State == GameStates.TimeStopped)
                    {
                        World.SetGameState(GameStates.TimeAdvancing);
                        World.DestroyEntity(_stateEntity);
                        World.DestroyEntity(commandEntity.First());
                    }
                        break;

                case GameStates.TimeAdvancing:
                    if (command.State == GameStates.TimeStopped)
                    {
                        EntityTemplate pauseTemplate = World.GetTemplate("pause");
                        World.SetGameState(GameStates.TimeStopped);
                        _stateEntity = ef.Create(pauseTemplate);
                        
                        World.DestroyEntity(commandEntity.First());
                    }
                    break;

                default:
                    break;
            }
        }
    }

    public void Render(SpriteBatch spriteBatch)
    {
        return;
    }
}