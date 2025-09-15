using HeartThrobFramework.Systems;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Core.ECS;

public class SystemManager
{
    private ISystem[] _systems = new ISystem[8];
    private int _systemCount;

    public void RegisterSystem<T>() where T : ISystem
    {
        _systems[_systemCount] = Activator.CreateInstance<T>();
        _systemCount++;
    }

    public void Update(World world, float deltaTime)
    {
        for (int i = 0; i < _systemCount; i++)
        {
            _systems[i].Update(world, deltaTime);
        }
    }

    public void Render(World world, SpriteBatch spriteBatch)
    {
        for (int i = 0; i < _systemCount; i++)
        {
            _systems[i].Render(world, spriteBatch);
        }
    }
}