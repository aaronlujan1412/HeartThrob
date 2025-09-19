namespace HeartThrobFramework.Core.ECS;

public interface IComponentPool
{
    int Count { get; }
    public void Remove(int entityId);
    public bool Has(int entityId);
    public IEnumerable<int> GetEntities();
}