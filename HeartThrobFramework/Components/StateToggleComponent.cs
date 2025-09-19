using HeartThrobFramework.GameData.StateEnums;

namespace HeartThrobFramework.Components
{
    public struct StateToggleComponent : IComponent
    {
        public GameStates State { get; private set; }


        public StateToggleComponent(GameStates state)
        {
            State = state;
        }
    }
}
