using HeartThrobFramework.Core.GameData;

namespace HeartThrobFramework.Components;

public struct GameStateComponent(GameStates state) : IComponent
{
    public GameStates State = state;
}