namespace HeartThrobFramework.Utils;

public class Config
{ 
    /// <summary>
    /// The maximum number of entities allowed in the world.
    /// This determines the size of the sparse array.
    /// </summary>
    public const int MaxEntities = 10000;

    // --- Component Capacities ---
    // The max number of each component type you expect to exist at once.

    public const int PlayerCapacity = 4;
    public const int EnemyCapacity = 500;
    public const int BulletCapacity = 2000;
    public const int CollisionCapacity = 2500;

    public const int SentinelValueForEntity = -1;
}
