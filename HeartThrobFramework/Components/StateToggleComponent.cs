using HeartThrobFramework.GameData.StateEnums;

namespace HeartThrobFramework.Components
{
    public struct StateToggleComponent : IComponent
    {
        public GameStates State { get; set; }

        public StateToggleComponent(GameStates state)
        {
            State = state;
        }
    }
}
