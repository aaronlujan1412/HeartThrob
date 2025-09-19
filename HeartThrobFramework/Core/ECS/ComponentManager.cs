using System.Reflection;
using HeartThrobFramework.Components;
using HeartThrobFramework.Utils;

namespace HeartThrobFramework.Core.ECS;

public class ComponentManager
{
    private const int DefaultCapacity = 50;
    private Dictionary<Type, IComponentPool> _componentPools = new Dictionary<Type, IComponentPool>();

    private SparseSet<T> CreateSetFor<T>() where T : IComponent
    {
        Type componentType = typeof(T);
        
        var attribute = componentType.GetCustomAttribute<ComponentCapacityAttribute>();
        
        int capacity = attribute?.Capacity ?? DefaultCapacity;
        
        return new SparseSet<T>(capacity, Config.MaxEntities);
    }

    public void RemoveAllComponentsFromEntity(int entityId)
    {
        foreach (var pool in _componentPools.Values)
        {
            if (pool.Has(entityId))
                pool.Remove(entityId);
        }
    }

    public void RegisterComponentType<T>() where T : IComponent
    {
        _componentPools.TryAdd(typeof(T), CreateSetFor<T>());
    }

    public bool HasComponent<T>(int entityId) where T : IComponent
    {
        if (!_componentPools.TryGetValue(typeof(T), out var pool))
        {
            return false;
        }
        
        return pool.Has(entityId);
    }

    public IComponentPool GetComponentPool(Type type)
    {
        if (!_componentPools.TryGetValue(type, out var pool))
        {
            throw new InvalidOperationException($"Component type {type.Name} is not registered, or does not exist.");
        }

        return pool;
    }

    public SparseSet<T> GetComponentPool<T>() where T : IComponent
    {
        if (!_componentPools.TryGetValue(typeof(T), out var pool))
        {
            throw new InvalidOperationException($"Component type {typeof(T)} is not registered, or does not exist.");
        }

        var poolAsSparse = pool as SparseSet<T>;
        
        return poolAsSparse ?? throw new InvalidOperationException($"Component type {typeof(T)} does not have a valid sparse set.");
    }

    public void AddComponent<T>(int entityId, T component) where T : IComponent
    {
        SparseSet<T> pool = GetComponentPool<T>();
        
        pool.Add(entityId, component);
    }
    
    public void RegisterAndAddComponent<T>(int entityId, T component) where T : IComponent
    {
        RegisterComponentType<T>();
        
        SparseSet<T> pool = GetComponentPool<T>();
        
        pool.Add(entityId, component);
    }

    public void RemoveComponent<T>(int entityId) where T : IComponent
    {
        SparseSet<T> pool = GetComponentPool<T>();

        pool.Remove(entityId);
    }

    public T GetComponent<T>(int entityId) where T : IComponent
    {
        SparseSet<T> pool = GetComponentPool<T>();

        return pool.GetComponent(entityId);

    }

    public void UpdateComponent<T>(int entityId, T component) where T : IComponent
    {
        SparseSet<T> pool = GetComponentPool<T>();
        
        pool.Set(entityId, component);
    }

    public IEnumerable<int> GetEntities<T>() where T: IComponent
    {
        return GetComponentPool<T>().GetEntities();
    }
}