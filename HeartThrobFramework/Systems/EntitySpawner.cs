using HeartThrobFramework.Core;
using HeartThrobFramework.Factories;
using HeartThrobFramework.GameData.Template;
using HeartThrobFramework.Utils;

namespace HeartThrobFramework.Systems
{
    public class EntitySpawner(EntityFactory _entityFactory)
    {
        public int SpawnMainCharacter(World World)
        {
            EntityTemplate mainCharacterTemplate = World.GetTemplate(EntityTemplateNames.Slime);

            int mainCharacter = _entityFactory.Create(mainCharacterTemplate);

            return mainCharacter;
        }
    }
}
