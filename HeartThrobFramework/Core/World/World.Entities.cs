namespace HeartThrobFramework.Core.World
{
    public partial class World
    {
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
    }
}
