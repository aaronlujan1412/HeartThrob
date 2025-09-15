using HeartThrobFramework.Core.GameData;

namespace HeartThrobFramework.Components;

public struct GameState(GameStates state) : IComponent
{
    public GameStates State = state;
}