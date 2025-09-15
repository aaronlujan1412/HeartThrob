using HeartThrobFramework.Components;
using Microsoft.Xna.Framework.Content;

namespace HeartThrobFramework.Utils;

public class EntityTemplateReader : ContentTypeReader<EntityTemplate>
{
    protected override EntityTemplate Read(ContentReader input, EntityTemplate existingInstance)
    {
        var template = new EntityTemplate();
        
        template.Name = input.ReadString();

        if (input.ReadBoolean())
        {
            var transform = new TransformData();

            transform.Position = input.ReadVector2();
            transform.Rotation = input.ReadSingle();
            transform.Scale = input.ReadSingle();
            template.Transform = transform;
        }

        if (input.ReadBoolean())
        {
            var velocity = new VelocityData();
            velocity.Value = input.ReadVector2();
            template.Velocity = velocity;
        }

        if (input.ReadBoolean())
        {
            var sprite = new SpriteData();
            sprite.Texture = input.ReadString();
            sprite.Color = input.ReadColor();
            sprite.IsEquipped = input.ReadBoolean();
            template.Sprite = sprite;
        }

        template.PlayerControlled = input.ReadBoolean();

        return template;
    }
}