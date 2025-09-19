using HeartThrobFramework.Core.World;
using HeartThrobFramework.Factories;
using HeartThrobFramework.GameData.Template;
using HeartThrobFramework.Utils;

namespace HeartThrobFramework.Systems
{
    public class EntitySpawner(EntityFactory _entityFactory)
    {
        public int SpawnMainCharacter()
        {
            int mainCharacter = _entityFactory.Create(EntityTemplateNames.Slime);

            return mainCharacter;
        }
    }
}
