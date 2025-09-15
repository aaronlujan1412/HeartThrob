using HeartThrobFramework.Systems;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Core.ECS;

public class SystemManager
{
    private readonly ISystem[] _systems = new ISystem[8];
    private readonly Dictionary<Type, ISystem> _systemsDictionary = new Dictionary<Type, ISystem>();
    private int _systemCount;

    public void RegisterSystem(ISystem system)
    {
        _systems[_systemCount] = system;
        _systemsDictionary[system.GetType()] = system;
        _systemCount++;
    }

    public T GetSystem<T>() where T : class, ISystem
    {
        _systemsDictionary.TryGetValue(typeof(T), out var system);
        return system as T;
    }

    public void Update(float deltaTime)
    {
        for (int i = 0; i < _systemCount; i++)
        {
            _systems[i].Update(deltaTime);
        }
    }

    public void Render(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < _systemCount; i++)
        {
            _systems[i].Render(spriteBatch);
        }
    }
}