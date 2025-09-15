namespace HeartThrobFramework.Components;

public struct ActionOrder : IComponent
{
    public int CurrentOrder;
    public float Speed = 1;

    public ActionOrder(int currentOrder, float speed = 1)
    {
        CurrentOrder = currentOrder;
        Speed = speed;
    }
}