namespace HeartThrobFramework.Utils;

[AttributeUsage(AttributeTargets.Struct)]
public class ComponentCapacityAttribute(int capacity) : Attribute
{
    public readonly int Capacity = capacity;
}