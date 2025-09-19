using HeartThrobFramework.Components;
using HeartThrobFramework.GameData.StateEnums;
using HeartThrobFramework.Core.ECS;

namespace HeartThrobFramework.Core.World;

public partial class World
{
    private readonly ComponentManager _cm;
    private readonly EntityManager _em;
    private readonly SystemManager _sm;

    public event Action<GameStates> OnGameStateChanged;

    public readonly int GameStateEntity;

    public GameStates CurrentState { get; private set; }

    public World()
    {
        _em = new EntityManager();
        _cm = new ComponentManager();
        _sm = new SystemManager();

        GameStateEntity = _em.CreateNewEntity();
        _cm.AddComponent(GameStateEntity, new GameStateComponent(GameStates.TimeAdvancing));
        CurrentState = GameStates.TimeAdvancing;
    }

    public void SetGameState(GameStates newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;

        _cm.UpdateComponent(GameStateEntity, new GameStateComponent(newState));
        OnGameStateChanged?.Invoke(newState);
    }
    
    public void Update(float deltaTime)
    {
        _sm.Update(deltaTime);
    }



}
