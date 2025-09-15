using System.Reflection;
using HeartThrobFramework.Components;
using HeartThrobFramework.Utils;

namespace HeartThrobFramework.Core;

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

    public IEnumerable<int> GetEntities<T1, T2>()
        where T1 : IComponent
        where T2 : IComponent
    {
        var entityArrays = new[]
        {
            GetComponentPool<T1>().GetEntities(),
            GetComponentPool<T2>().GetEntities()
        }.OrderBy(e => e.Count()).ToArray();

        foreach (int entity in entityArrays[0])
        {
            if (entityArrays[1].Contains(entity))
            {
                yield return entity;
            }
        }
    }

    public IEnumerable<int> GetEntities<T1, T2, T3>()
        where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
    {
        var entityArrays = new[]
        {
            GetComponentPool<T1>().GetEntities(),
            GetComponentPool<T2>().GetEntities(),
            GetComponentPool<T3>().GetEntities()
        }.OrderBy(e => e.Count()).ToArray();

        foreach (int entity in entityArrays[0])
        {
            if (entityArrays[1].Contains(entity) && entityArrays[2].Contains(entity))
            {
                yield return entity;
            }
        }
    }
    
    public IEnumerable<int> GetEntities<T1, T2, T3, T4>()
        where T1 : IComponent
        where T2 : IComponent
        where T3 : IComponent
        where T4 : IComponent
    {
        var entityArrays = new[]
        {
            GetComponentPool<T1>().GetEntities(),
            GetComponentPool<T2>().GetEntities(),
            GetComponentPool<T3>().GetEntities(),
            GetComponentPool<T4>().GetEntities()
        }.OrderBy(e => e.Count()).ToArray();

        foreach (int entity in entityArrays[0])
        {
            if (entityArrays[1].Contains(entity) && entityArrays[2].Contains(entity)  && entityArrays[3].Contains(entity))
            {
                yield return entity;
            }
        }
    }
}