using HeartThrobFramework.Components;
using HeartThrobFramework.Core.ECS;
using HeartThrobFramework.Utils;
using System.Reflection;

namespace HeartThrobTests;

public class TestHelpers
{
    public static SparseSet<T> CreateSetFor<T>() where T : IComponent
    {
        int DefaultCapacity = 150; 
        
        Type type = typeof(T);

        var attribute = type.GetCustomAttribute<ComponentCapacityAttribute>();

        int capacity = attribute?.Capacity ?? DefaultCapacity;

        return new SparseSet<T>(capacity, DefaultCapacity);
    }
    
    public class TheoryData<T1, T2> : TheoryData
    {
        public void Add(T1 t1, T2 t2)
        {
            AddRow(t1, t2);
        }
    }

    public class SparseSetPlayerControlledComponentTestData : TheoryData<int, IComponent>
    {
        public SparseSetPlayerControlledComponentTestData()
        {
            Add(0, new PlayerControlledComponent());
            Add(30, new PlayerControlledComponent());
            Add(100, new PlayerControlledComponent());
        }
    }

//    public class SparseSetPositionComponentTestData : TheoryData<int, IComponent>
//    {
//        public SparseSetPositionComponentTestData()
//        {
//            Add(0, new Position(0, 0));
//            Add(30, new Position(1, 0));
//            Add(100, new Position(1, 1));
//        }
//    }
}