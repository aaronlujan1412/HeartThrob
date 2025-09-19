using HeartThrobFramework.Systems;

namespace HeartThrobFramework.Core.World
{
    public partial class World
    {
        public void RegisterSystem(ISystem system)
        {
            system.World = this;
            _sm.RegisterSystem(system);
        }

        public T GetSystem<T>() where T : class, ISystem
        {
            return _sm.GetSystem<T>();
        }
    }
}
