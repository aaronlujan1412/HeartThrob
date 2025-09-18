using HeartThrobFramework.Components;
using HeartThrobFramework.Core;
using HeartThrobFramework.Factories;
using HeartThrobFramework.GameData.StateEnums;
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

            switch (command.State)
            {
                case GameStates.TimeStopped:
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