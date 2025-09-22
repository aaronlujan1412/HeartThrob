using HeartThrobFramework.Core.World;
using HeartThrobFramework.GameData.StateEnums;

namespace HeartThrobFramework.Systems
{
    public class UISystem (EntitySpawner entitySpawner): ISystem
    {
        private World _world = null!;
        public World World
        {
            get => _world;
            set
            {
                if (_world != null) _world.OnGameStateChanged -= HandleGameStateChange;

                _world = value;

                _world.OnGameStateChanged += HandleGameStateChange;
            }
        }

        private readonly EntitySpawner _entitySpawner = entitySpawner;
        private int _pauseEntity = -1;


        private void HandleGameStateChange(GameStates newState)
        {
            switch (newState)
            {
                case GameStates.TimeStopped:
                    _pauseEntity = _entitySpawner.SpawnPauseMenu();
                    break;
                case GameStates.TimeAdvancing:
                    _world.DestroyEntity(_pauseEntity);
                    _pauseEntity = -1;
                    break;
            }
        }

        public void Update(float deltaTime)
        {
            return;
        }
    }
}
