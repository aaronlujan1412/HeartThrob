using HeartThrobFramework.Components;

namespace HeartThrobFramework.Core.World
{
    public partial class World
    {
         private IEnumerable<int> QueryMultiple(params Type[] componentTypes)
        {
            if (componentTypes.Length == 0) yield break;

            var pools = componentTypes.Select(t => _cm.GetComponentPool(t)).ToArray();

            Array.Sort(pools, (a, b) => a.Count.CompareTo(b.Count));

            var smallestPool = pools[0];

            foreach (var entity in smallestPool.GetEntities())
            {
                bool hasAll = true;
                for (int i = 1; i < pools.Length; i++)
                {
                    if (!pools[i].Has(entity))
                    {
                        hasAll = false;
                        break;
                    }
                }

                if (hasAll)
                    yield return entity;
            }
        }

        public IEnumerable<int> Query<T1>() where T1 : IComponent
        {
            foreach (var e in _cm.GetComponentPool<T1>().GetEntities())
                yield return e;
        }

        public IEnumerable<int> Query<T1, T2>()
            where T1 : IComponent
            where T2 : IComponent
        {
            return QueryMultiple(typeof(T1), typeof(T2));
        }

        public IEnumerable<int> Query<T1, T2, T3>()
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
        {
            return QueryMultiple(typeof(T1), typeof(T2), typeof(T3));
        }

        public IEnumerable<int> Query<T1, T2, T3, T4>()
            where T1 : IComponent
            where T2 : IComponent
            where T3 : IComponent
            where T4 : IComponent
        {
            return QueryMultiple(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }
    }
}
