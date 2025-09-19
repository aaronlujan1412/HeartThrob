using HeartThrobFramework.Components;

namespace HeartThrobFramework.Core.World
{
    public partial class World
    {
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
    }
}
