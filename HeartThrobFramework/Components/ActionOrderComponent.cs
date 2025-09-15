namespace HeartThrobFramework.Components;

public struct ActionOrderComponent : IComponent
{
    public int CurrentOrder;
    public float Speed = 1;

    public ActionOrderComponent(int currentOrder, float speed = 1)
    {
        CurrentOrder = currentOrder;
        Speed = speed;
    }
}