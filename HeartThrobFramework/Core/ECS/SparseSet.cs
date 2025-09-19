using HeartThrobFramework.Components;
using HeartThrobFramework.Utils;

namespace HeartThrobFramework.Core.ECS;


public class SparseSet<T> : IComponentPool where T : IComponent
{
    private const int SentinelValue = Config.SentinelValueForEntity;
    
    private readonly int _capacity;
    private readonly int _maxEntities;
    
    private readonly int[] _sparse;
    private readonly int[] _entities;
    private readonly T[] _components;

    public int Count { get; private set; } = 0;

    public SparseSet(int componentCapacity, int maxEntities)
    {
        _capacity = componentCapacity;
        _maxEntities = maxEntities;

        _sparse = new int[_maxEntities];
        Array.Fill(_sparse, SentinelValue);
        _entities = new int[_capacity];
        _components = new T[_capacity];
    }

    public void Add(int entityId, T component)
    {
        if (entityId < 0 || entityId >= _maxEntities)
        {
            throw new ArgumentOutOfRangeException(nameof(entityId));
        }
        
        if (Count >= _capacity)
        {
            throw new InvalidOperationException("Cannot add more entities.");
        }

        if (Has(entityId))
        {
            Set(entityId, component);
            return;
        }

        _sparse[entityId] = Count;
        _components[Count] = component;
        _entities[Count] = entityId;
        Count++;
    }

    public void Remove(int entityToRemove)
    {
        if (entityToRemove >= _maxEntities)
        {
            throw new ArgumentOutOfRangeException(nameof(entityToRemove));
        }
        
        if (!Has(entityToRemove))
        {
            throw new InvalidOperationException($"Entity {entityToRemove} does not have a {typeof(T)} component.");
        }
        
        int lastEntityInDense = _entities[Count - 1];
        if (entityToRemove != lastEntityInDense)
        {
            int entityToBeRemovedDenseIndex = _sparse[entityToRemove];
        
            _entities[entityToBeRemovedDenseIndex] = lastEntityInDense;
            _components[entityToBeRemovedDenseIndex] = _components[Count - 1];
            _sparse[lastEntityInDense] = entityToBeRemovedDenseIndex;
            
        }
        _entities[Count - 1] = SentinelValue;
        _components[Count - 1] = default;
        _sparse[entityToRemove] = SentinelValue;
        
        Count--;
    }
    
    public void Set(int entityId, T component)
    {
        if (entityId < 0 || entityId >= _maxEntities)
        {
            throw new ArgumentOutOfRangeException(nameof(entityId));
        }
        if (!Has(entityId))
        {
            throw new InvalidOperationException($"Entity {entityId} does not have a {typeof(T)} component.");
        }
        
        _components[_sparse[entityId]] = component;
    }

    public T GetComponent(int entityId)
    {
        if (!Has(entityId))
        {
            throw new InvalidOperationException($"Entity {entityId} does not have a {typeof(T)} component.");
        }
        return _components[_sparse[entityId]];
    }

    public bool Has(int entityId)
    {
        if (entityId >= _maxEntities)
            return false;
        
        int denseIndex = _sparse[entityId];
        
        return denseIndex != SentinelValue && denseIndex < Count && _entities[denseIndex] == entityId;
    }

    public IEnumerable<int> GetEntities()
    {
        return new ArraySegment<int>(_entities, 0, Count);
    }
}