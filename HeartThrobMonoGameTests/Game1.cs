using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HeartThrobFramework.Core;
using HeartThrobFramework.Components;
using HeartThrobFramework.Core.GameData;
using HeartThrobFramework.Systems;
using HeartThrobFramework.GameData.Template;

namespace HeartThrobMonoGameTests;

public class Game1 : Game
{
    private World _world;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private EntityFactory _entityFactory;
    private int _pauseEntity = -1;


    public Game1()
    {
        _world = new World();
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        _entityFactory = new EntityFactory(_world, Content);
        IsMouseVisible = true;
        
        
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
    //             if (keyboardState.IsKeyDown(Keys.E) && _lastKeyboardState.IsKeyUp(Keys.E) && !sprite.IsEquipped)
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

        _world.RegisterSystem<RenderSystem>();
        _world.RegisterSystem<InputSystem>();
        _world.RegisterSystem<MovementSystem>();
        _world.RegisterSystem<GameStateSystem>();
        
        int worldEntity = _world.CreateEntity();
        _world.AddComponent<GameStateComponent>(worldEntity, new GameStateComponent(GameStates.TimeAdvancing));

        var pauseTemplate = Content.Load<EntityTemplate>("Entities/pause");
        
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);


        var slimeTemplate = Content.Load<EntityTemplate>("Entities/slime");

        int slimeEntity = _entityFactory.Create(slimeTemplate);

    }



    // public void CreateMainPlayer()
    // {
    //     foreach (var file in Directory.GetFiles(templatePath, "*.json"))
    //     {
    //         string jsonString = File.ReadAllText(file);
    //         var template = JsonSerializer.Deserialize<EntityTemplate>(jsonString);
    //         
    //         string templateName = Path.GetFileNameWithoutExtension(file);
    //         _templates.Add(templateName, template);
    //     }
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
        } else if (newState == GameStates.TimeAdvancing)
        {
            if (_pauseEntity != -1)
            {
                _world.DestroyEntity(_pauseEntity);
                _pauseEntity = -1;
            }
        }
    }
    
    public event EntityEventHandler MainPlayerCreated;
    
    public delegate void EntityEventHandler(int entityId);

    protected override void Update(GameTime gameTime)
    {
        
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        
        _world.Update(delta);
        // EquipPlayer(delta);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

//        if (_world.State == GameStates.TimeStopped)
//        {
//            foreach (var entity in _world.Query<Clickable>())
//            {
//                _world.AddComponent;
//            }
//        }
        
        
        _world.Render(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}