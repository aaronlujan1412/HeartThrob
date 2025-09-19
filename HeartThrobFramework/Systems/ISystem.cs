using HeartThrobFramework.Core;

namespace HeartThrobFramework.Systems;

public interface ISystem
{
    public World World { get; set; }

    void Update(float deltaTime);
}