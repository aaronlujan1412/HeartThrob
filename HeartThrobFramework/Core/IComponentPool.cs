namespace HeartThrobFramework.Core;

public interface IComponentPool
{
    public void Remove(int entityId);
    public bool Has(int entityId);
}