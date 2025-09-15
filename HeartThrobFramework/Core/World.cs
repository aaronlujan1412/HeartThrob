using HeartThrobFramework.Components;
using System.Reflection;
using HeartThrobFramework.Systems;
using Microsoft.Xna.Framework.Graphics;
using HeartThrobFramework.GameData.StateEnums;

namespace HeartThrobFramework.Core;

public class World
{
    public event Action<GameStates> OnGameStateChanged;
    
    ComponentManager _cm = new ComponentManager();
    EntityManager _em = new EntityManager();
    SystemManager _sm = new SystemManager();

    public GameStates State = GameStates.TimeAdvancing;

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

    public void RegisterSystem<T>() where T : ISystem
    {
        _sm.RegisterSystem<T>();
    }

    public void UpdateGameState(GameStates newState)
    {
        State = newState;
        OnGameStateChanged?.Invoke(newState);
    }
    
    public void Update(float deltaTime)
    {
        _sm.Update(this, deltaTime);
    }

    public void Render(SpriteBatch spriteBatch)
    {
        _sm.Render(this, spriteBatch);
    }

    public IEnumerable<int> Query<T>() where T : IComponent
    {
        return _cm.GetEntities<T>();
    }

    public IEnumerable<int> Query<T1, T2>()
        where T1 : IComponent
        where T2 : IComponent
    {
        return _cm.GetEntities<T1, T2>();
    }
    
    public IEnumerable<int> Query<T1, T2, T3>() 
        where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
    {
        return _cm.GetEntities<T1, T2, T3>();
    }
    
    public IEnumerable<int> Query<T1, T2, T3, T4>()
        where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent
    {
        return _cm.GetEntities<T1, T2, T3, T4>();
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
}
