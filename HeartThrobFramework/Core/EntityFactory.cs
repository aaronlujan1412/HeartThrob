using HeartThrobFramework.Components;
using HeartThrobFramework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HeartThrobFramework.Core;

public class EntityFactory
{
    private readonly World _world;
    private readonly ContentManager _contentManager;

    public EntityFactory(World world, ContentManager content)
    {
        _world = world;
        _contentManager = content;
    }

    public int Create(EntityTemplate template)
    {
        int entityId = _world.CreateEntity();

        if (template.Transform.HasValue)
        {
            var transformData = template.Transform.Value;

            var transformComponent = new Transform
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

            var velocityComponent = new Velocity
            {
                Value = velocityData.Value,
            };
            _world.AddComponent(entityId, velocityComponent);
        }

        if (template.Sprite.HasValue)
        {
            var  spriteData = template.Sprite.Value;
            
            var texture = _contentManager.Load<Texture2D>(spriteData.Texture);

            var spriteComponent = new Sprite
            {
                Texture = texture,
                Color = spriteData.Color,
                IsEquipped = spriteData.IsEquipped
            };
            _world.AddComponent(entityId, spriteComponent);
        }

        if (template.PlayerControlled)
        {
            _world.AddComponent(entityId, new PlayerControlled());
        }

        return entityId;
    }
}