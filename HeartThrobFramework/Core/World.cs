using HeartThrobFramework.Components;
using System.Reflection;
using HeartThrobFramework.Systems;
using HeartThrobFramework.GameData.StateEnums;
using HeartThrobFramework.Core.ECS;
using Microsoft.Xna.Framework.Content;
using HeartThrobFramework.GameData.Template;

namespace HeartThrobFramework.Core;

public class World
{
    private readonly ComponentManager _cm;
    private readonly EntityManager _em;
    private readonly SystemManager _sm;
    private readonly TemplateManager _tm;

    public event Action<GameStates> OnGameStateChanged;

    public readonly int GameStateEntity;

    public GameStates CurrentState { get; private set; }

    public World()
    {
        _em = new EntityManager();
        _cm = new ComponentManager();
        _sm = new SystemManager();
        _tm = new TemplateManager();

        GameStateEntity = _em.CreateNewEntity();
        _cm.AddComponent(GameStateEntity, new GameStateComponent(GameStates.TimeAdvancing));
        CurrentState = GameStates.TimeAdvancing;
    }

    public int CreateEntity()
    {
        int newEntity = _em.CreateNewEntity();
        return newEntity;
    }
    
    public void DestroyEntity(int entity)
    {
        _cm.RemoveAllComponentsFromEntity(entity);
        _em.RemoveEntity(entity);
    }
    
    public IEnumerable<int> GetAliveEntities()
    {
        return _em.GetAliveEntities();
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

        _cm.UpdateComponent(GameStateEntity, new GameStateComponent(newState));
        OnGameStateChanged?.Invoke(newState);
    }
    
    public void Update(float deltaTime)
    {
        _sm.Update(deltaTime);
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

     private IEnumerable<int> QueryMultiple(params Type[] componentTypes)
    {
        if (componentTypes.Length == 0) yield break;

        var pools = componentTypes.Select(t => _cm.GetComponentPool(t)).ToArray();

        Array.Sort(pools, (a, b) => a.Count.CompareTo(b.Count));

        var smallestPool = pools[0];

        foreach (var entity in smallestPool.GetEntities())
        {
            bool hasAll = true;
            for (int i = 1; i < pools.Length; i++)
            {
                if (!pools[i].Has(entity))
                {
                    hasAll = false;
                    break;
                }
            }

            if (hasAll)
                yield return entity;
        }
    }

    public IEnumerable<int> Query<T1>() where T1 : IComponent
    {
        foreach (var e in _cm.GetComponentPool<T1>().GetEntities())
            yield return e;
    }

    public IEnumerable<int> Query<T1, T2>()
        where T1 : IComponent
        where T2 : IComponent
    {
        return QueryMultiple(typeof(T1), typeof(T2));
    }

    public IEnumerable<int> Query<T1, T2, T3>()
        where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
    {
        return QueryMultiple(typeof(T1), typeof(T2), typeof(T3));
    }

    public IEnumerable<int> Query<T1, T2, T3, T4>()
        where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent
    {
        return QueryMultiple(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
    }
}
