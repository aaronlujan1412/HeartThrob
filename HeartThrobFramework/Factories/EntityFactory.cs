using HeartThrobFramework.Components;
using HeartThrobFramework.Core.ECS;
using HeartThrobFramework.Core.World;
using HeartThrobFramework.GameData.Template;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Factories;

public class EntityFactory(World world, ContentManager content, TemplateManager templateManager)
{
    private readonly World _world = world;
    private readonly ContentManager _contentManager = content;
    private readonly TemplateManager _templateManager = templateManager;

    public int Create(string templateName)
    {
        EntityTemplate template = _templateManager.GetTemplate(templateName);

        int entityId = _world.CreateEntity();

        if (template.Transform.HasValue)
        {
            var transformData = template.Transform.Value;

            var transformComponent = new TransformComponent
            {
                Position = transformData.Position,
                Rotation = transformData.Rotation,
                Scale = transformData.Scale
            };
            _world.AddComponent(entityId, transformComponent);
        }

        if (template.Velocity.HasValue)
        {
            var velocityData = template.Velocity.Value;

            var velocityComponent = new VelocityComponent
            {
                Value = velocityData.Value,
            };
            _world.AddComponent(entityId, velocityComponent);
        }

        if (template.Sprite.HasValue)
        {
            var  spriteData = template.Sprite.Value;
            
            var texture = _contentManager.Load<Texture2D>(spriteData.Texture);

            var spriteComponent = new SpriteComponent
            {
                Texture = texture,
                Color = spriteData.Color,
                IsEquipped = spriteData.IsEquipped
            };
            _world.AddComponent(entityId, spriteComponent);
        }

        if (template.PlayerControlled)
        {
            _world.AddComponent(entityId, new PlayerControlledComponent());
        }

        return entityId;
    }
}