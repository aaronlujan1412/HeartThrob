using HeartThrobFramework.Core;

namespace HeartThrobFramework.Systems;

public class TurnBasedSystem : ISystem
{
    public World World { get; set; } = null!;
    public void Update(float deltaTime)
    {
        return;
//        var entities = world.Query<ActionOrder>();
    }
}