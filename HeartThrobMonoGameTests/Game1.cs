using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HeartThrobFramework.Components;
using HeartThrobFramework.Systems;
using HeartThrobFramework.GameData.StateEnums;
using HeartThrobFramework.Factories;
using HeartThrobFramework.Managers;
using HeartThrobFramework.Core.World;
using HeartThrobFramework.Core.ECS;

namespace HeartThrobMonoGameTests;

public class Game1 : Game
{
    private readonly World _world;
    private readonly GraphicsDeviceManager _graphics;
    private readonly InputManager _inputManager;
    private TemplateManager _templateManager;
    private EntityFactory _entityFactory;
    private SpriteBatch _spriteBatch;
    private EntitySpawner _spawner;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _world = new World();
        _inputManager = new InputManager();
    }
    
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
        _world.RegisterComponent<StateToggleComponent>();

        _world.AddComponent<GameStateComponent>(_world.GameStateEntity, new GameStateComponent(GameStates.TimeAdvancing));
        _world.SetGameState(GameStates.TimeAdvancing);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _templateManager = new TemplateManager();
        _templateManager.LoadAllTemplates(Content);

        _entityFactory = new EntityFactory(_world, Content, _templateManager);

        _spawner = new EntitySpawner(_entityFactory);
        _spawner.SpawnMainCharacter();

        var renderSystem = new RenderSystem(_spriteBatch);
        _world.RegisterSystem(renderSystem);

        var inputSystem = new InputSystem(_inputManager);
        _world.RegisterSystem(inputSystem);

        var movementSystem = new MovementSystem();
        _world.RegisterSystem(movementSystem);

        var gameStateSystem = new GameStateSystem();
        _world.RegisterSystem(gameStateSystem);

        var uiSystem = new UISystem(_spawner);
        _world.RegisterSystem(uiSystem);
    }

    protected override void Update(GameTime gameTime)
    {
        _inputManager.Update();
        _world.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _world.GetSystem<RenderSystem>().Render(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}