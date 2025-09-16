using HeartThrobFramework.Components;
using System.Reflection;
using HeartThrobFramework.Systems;
using Microsoft.Xna.Framework.Graphics;
using HeartThrobFramework.GameData.StateEnums;
using HeartThrobFramework.Core.ECS;
using Microsoft.Xna.Framework.Content;
using HeartThrobFramework.GameData.Template;
using HeartThrobFramework.Utils;

namespace HeartThrobFramework.Core;

public class World
{
    ComponentManager _cm;
    EntityManager _em;
    SystemManager _sm;
    TemplateManager _tm;

    public event Action<GameStates> OnGameStateChanged;
    public event Action OnEscPressed;


    private readonly int _gameStateEntity;

    public GameStates CurrentState { get; private set; }

    public World()
    {
        _em = new EntityManager();
        _cm = new ComponentManager();
        _sm = new SystemManager();
        _tm = new TemplateManager();

        _sm.GetSystem<InputSystem>().OnMenuButtonPressed += HandleMenuButtonPressed;

        _gameStateEntity = _em.CreateNewEntity();
        _cm.AddComponent(_gameStateEntity, new GameStateComponent(GameStates.TimeAdvancing));
        CurrentState = GameStates.TimeAdvancing;
    }

    public int CreateEntity()
    {
        int newEntity = _em.CreateNewEntity();
        return newEntity;
    }

    public void HandleMenuButtonPressed(string menuButton)
    {
        if (menuButton == "esc")
        {
            OnEscPressed?.Invoke();
        }
    }
    
    public void DestroyEntity(int entity)
    {
        _cm.RemoveAllComponentsFromEntity(entity);
        _em.RemoveEntity(entity);
    }
    
    public int[] GetAliveEntities()
    {
        return _em.GetAliveEntities().ToArray();
    }

    public void RegisterComponent<T>() where T : IComponent
    {
        _cm.RegisterComponentType<T>();
    }

    public void AddComponent<T>(int entityId, T component) where T : IComponent
    {
        if (!_em.IsAlive(entityId))
        {
            throw new InvalidOperationException($"Entity {entityId} does not exist in current context.");
        }

        _cm.AddComponent(entityId, component);
    }

    public void UpdateComponent<T>(int entityId, T component) where T : IComponent
    {
        if (!_em.IsAlive(entityId))
        {
            throw new InvalidOperationException($"Entity {entityId} does not exist in current context.");
        }

        _cm.UpdateComponent(entityId, component);
    }

    public void RemoveComponent<T>(int entityId) where T : IComponent
    {
        if (!_em.IsAlive(entityId))
        {
            throw new InvalidOperationException($"Entity {entityId} does not exist in current context.");
        }

        _cm.RemoveComponent<T>(entityId);
    }

    public bool HasComponent<T>(int entityId) where T : IComponent
    {
        if (!_em.IsAlive(entityId))
        {
            throw new InvalidOperationException($"Entity {entityId} does not exist in current context.");
        }

        return _cm.HasComponent<T>(entityId);
    }

    public T GetComponent<T>(int entityId) where T : IComponent
    {
        if (!_em.IsAlive(entityId))
        {
            throw new InvalidOperationException($"Entity {entityId} does not exist in current context.");
        }

        return _cm.GetComponent<T>(entityId);
    }

    public void RegisterSystem(ISystem system)
    {
        system.World = this;
        _sm.RegisterSystem(system);
    }

    public T GetSystem<T>() where T : class, ISystem
    {
        return _sm.GetSystem<T>();
    }

    public void SetGameState(GameStates newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;

        _cm.UpdateComponent(_gameStateEntity, new GameStateComponent(newState));
        OnGameStateChanged?.Invoke(newState);
    }
    
    public void Update(float deltaTime)
    {
        _sm.Update(deltaTime);
    }

    public void Render(SpriteBatch spriteBatch)
    {
        _sm.Render(spriteBatch);
    }

    public IEnumerable<int> Query<T>() where T : IComponent
    {
        return _cm.GetEntities<T>();
    }

    public IEnumerable<int> Query<T1, T2>()
        where T1 : IComponent
        where T2 : IComponent
    {
        var entityArrays = new[]
        {
            _cm.GetComponentPool<T1>().GetEntities(),
            _cm.GetComponentPool<T2>().GetEntities()
        }.OrderBy(e => e.Count()).ToArray();

        foreach (int entity in entityArrays[0])
        {
            if (entityArrays[1].Contains(entity))
            {
                yield return entity;
            }

            if (HasComponent<T1>(entity) && HasComponent<T2>(entity))
            {
                yield return entity;
            }
        }
    }

    public IEnumerable<int> Query<T1, T2, T3>()
        where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
    {
        var entityArrays = new[]
        {
            _cm.GetComponentPool<T1>().GetEntities(),
            _cm.GetComponentPool<T2>().GetEntities(),
            _cm.GetComponentPool<T3>().GetEntities()
        }.OrderBy(e => e.Count()).ToArray();

        foreach (int entity in entityArrays[0])
        {
            if (entityArrays[1].Contains(entity) && entityArrays[2].Contains(entity))
            {
                yield return entity;
            }
        }
    }
    
    public IEnumerable<int> Query<T1, T2, T3, T4>()
        where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent
    {
        var entityArrays = new[]
        {
            _cm.GetComponentPool<T1>().GetEntities(),
            _cm.GetComponentPool<T2>().GetEntities(),
            _cm.GetComponentPool < T3 >().GetEntities(),
            _cm.GetComponentPool < T4 >().GetEntities()
        }.OrderBy(e => e.Count()).ToArray();

        foreach (int entity in entityArrays[0])
        {
            if (entityArrays[1].Contains(entity) && entityArrays[2].Contains(entity)  && entityArrays[3].Contains(entity))
            {
                yield return entity;
            }
        }
    }

    public static IEnumerable<Type> GetAllComponentTypes(Assembly assembly)
    {
        var componentInterface = typeof(IComponent);

        try
        {
            return assembly.GetTypes()
                .Where(t => t.IsValueType && componentInterface.IsAssignableFrom(t));
        }
        catch (ReflectionTypeLoadException)
        {
            return Enumerable.Empty<Type>();
        }
    }

    public void LoadTemplates(ContentManager content)
    {
        _tm.LoadAllTemplates(content);
    }

    public EntityTemplate GetTemplate(string templateName)
    {
        return _tm.GetTemplate(templateName);
    }
}
