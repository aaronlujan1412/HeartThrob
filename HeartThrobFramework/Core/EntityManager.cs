using HeartThrobFramework.Utils;

namespace HeartThrobFramework.Core;

public class EntityManager
{
    private readonly int[] _deadEntities = new int[Config.MaxEntities];
    private readonly bool[] _aliveOrDead = new bool[Config.MaxEntities];
    
    private const int SentinelValue = Config.SentinelValueForEntity;
    private int _nextEntityId;
    private int _deadEntityCount;

    public EntityManager()
    {
        Array.Fill(_deadEntities, SentinelValue);
    }

    public int CreateNewEntity()
    {
        int newEntity;

        if (_nextEntityId >= Config.MaxEntities && _deadEntityCount == 0)
        {
            throw new OverflowException("Entity list is full.");
        }
        
        if (_deadEntityCount >= 1)
        {
            newEntity = _deadEntities[_deadEntityCount - 1];
            _deadEntityCount--;
        }
        else
        {
            newEntity = _nextEntityId;
            _nextEntityId++;
        }

        _aliveOrDead[newEntity] = true;
        return newEntity;
    }

    public void RemoveEntity(int entityId)
    {
        if (entityId >= Config.MaxEntities)
        {
            throw new InvalidOperationException("Cannot remove an entity that is out of bounds.");
        }
        if (_deadEntityCount >= Config.MaxEntities)
        {
            throw new InvalidOperationException("Dead entity list is full.");
        }
        
        _deadEntities[_deadEntityCount] = entityId;
        _deadEntityCount++;
        _aliveOrDead[entityId] = false;
    }

    public bool IsAlive(int entityId)
    {
        if (entityId >= Config.MaxEntities)
        {
            throw new InvalidOperationException("Cannot remove an entity that is out of bounds.");
        }

        return _aliveOrDead[entityId];
    }

    public IEnumerable<int> GetAliveEntities()
    {
        for (int i = 0; i < _nextEntityId; i++)
        {
            if (_aliveOrDead[i])
            {
                yield return i;
            }
        }
    }
}