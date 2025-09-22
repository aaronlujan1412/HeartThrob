using HeartThrobFramework.Factories;
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

        public int SpawnPauseMenu()
        {
            int pauseMenu = _entityFactory.Create(EntityTemplateNames.Pause);

            return pauseMenu;
        }
    }
}
