using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HeartThrobFramework.Core;
using HeartThrobFramework.Components;
using HeartThrobFramework.Systems;
using HeartThrobFramework.GameData.Template;
using HeartThrobFramework.GameData.StateEnums;
using HeartThrobFramework.Factories;

namespace HeartThrobMonoGameTests;

public class Game1 : Game
{
    private readonly World _world;
    private readonly GraphicsDeviceManager _graphics;
    private readonly EntityFactory _entityFactory;
    private SpriteBatch _spriteBatch;
    private int _pauseEntity = -1;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _world = new World();
        _entityFactory = new EntityFactory(_world, Content);
        
        _world.OnGameStateChanged += HandleGameStateChange;
    }

    // public void EquipPlayer(float deltaTime)
    // {
    //     foreach (var entityId in _world.GetAliveEntities())
    //     {
    //         if (_world.HasComponent<PlayerControlled>(entityId))
    //         {
    //             var sprite = _world.GetComponent<Sprite>(entityId);
    //             var keyboardState = Keyboard.GetState();
    //
    //             {
    //                 sprite.Texture = Content.Load<Texture2D>("slimeManEquipped");
    //                 sprite.IsEquipped = true;
    //             } else if (keyboardState.IsKeyDown(Keys.E) && _lastKeyboardState.IsKeyUp(Keys.E) && sprite.IsEquipped)
    //             {
    //                 sprite.Texture = Content.Load<Texture2D>("slimeMan");
    //                 sprite.IsEquipped = false;
    //             }
    //             
    //             _lastKeyboardState =  keyboardState;
    //             _world.UpdateComponent(entityId, sprite);
    //         }
    //     }
    // }

    
    protected override void Initialize()
    {

        _world.RegisterComponent<TransformComponent>();
        _world.RegisterComponent<VelocityComponent>();
        _world.RegisterComponent<SpriteComponent>();
        _world.RegisterComponent<PlayerControlledComponent>();
        _world.RegisterComponent<EquippableComponent>();
        _world.RegisterComponent<InventoryComponent>();
        _world.RegisterComponent<EquipmentComponent>();
        _world.RegisterComponent<CollisionComponent>();
        _world.RegisterComponent<GameStateComponent>();
        _world.RegisterComponent<ClickableComponent>();

        var renderSystem = new RenderSystem(_spriteBatch);
        _world.RegisterSystem(renderSystem);

        var inputSystem = new InputSystem();
        _world.RegisterSystem(inputSystem);

        var movementSystem = new MovementSystem();
        _world.RegisterSystem(movementSystem);

        var gameStateSystem = new GameStateSystem();
        _world.RegisterSystem(gameStateSystem);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _world.LoadTemplates(Content);
    }



    // public void CreateMainPlayer()
    // {
    //     
    //     var slimeMan = Content.Load<Texture2D>("slimeMan");
    //     int mainPlayer = _world.CreateEntity();
    //     _world.AddComponent(mainPlayer, new Transform { Position = Vector2.Zero} );
    //     _world.AddComponent(mainPlayer, new Velocity { Value = Vector2.Zero } );
    //     _world.AddComponent(mainPlayer, new Sprite { Texture = slimeMan, Color = Color.White });
    //     _world.AddComponent(mainPlayer, new PlayerControlled());
    //     _world.AddComponent(mainPlayer, new Equipment());
    //     _world.AddComponent(mainPlayer, new Inventory());
    //
    //     MainPlayerCreated?.Invoke(mainPlayer);
    // }

    // public void CreateSwordInStone()
    // {
    //     var sword = Content.Load<Texture2D>("swordInStone");
    //     int swordEntity = _world.CreateEntity();
    //     _world.AddComponent(swordEntity, new Transform { Position = new Vector2(x:200,  y:200) } );
    //     _world.AddComponent(swordEntity, new Velocity { Value = Vector2.Zero } );
    //     _world.AddComponent(swordEntity, new Sprite { Texture = sword, Color = Color.White });
    //     _world.AddComponent(swordEntity, new Equippable());
    // }

    // public void EquipMainPlayer(int entityId)
    // {
    //     var sprite =  _world.GetComponent<Sprite>(entityId);
    //     sprite.Texture = Content.Load<Texture2D>("slimeManEquipped");
    //     sprite.IsEquipped = true;
    //     
    //     _world.UpdateComponent(entityId, sprite);
    // }

    public void HandleGameStateChange(GameStates newState)
    {
        if (newState == GameStates.GameOver)
        {
            Exit();
        }

        if (newState == GameStates.TimeStopped)
        {
            var pauseTemplate = Content.Load<EntityTemplate>("Entities/pause");
            _pauseEntity = _entityFactory.Create(pauseTemplate);
        } else if (_pauseEntity != -1)
        {
            _world.DestroyEntity(_pauseEntity);
            _pauseEntity = -1;
        }
    }

    protected override void Update(GameTime gameTime)
    {
        if (_world.CurrentState == GameStates.TimeAdvancing)
        {
            _world.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }
        else
        {
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _world.GetSystem<RenderSystem>().Render(_spriteBatch);


        if (_world.CurrentState == GameStates.TimeStopped && _pauseEntity != -1)
        {
            _world.GetSystem<RenderSystem>().RenderEntity(_spriteBatch, _pauseEntity);
        }
        

        _world.Render(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}