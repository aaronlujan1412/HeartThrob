using System;
using HeartThrobFramework.Components;
using HeartThrobFramework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace HeartThrobFramework.ContentPipeline;

[ContentTypeWriter]
public class EntityTemplateWriter : ContentTypeWriter<EntityTemplate>
{
    protected override void Write(ContentWriter output, EntityTemplate value)
    {
        output.Write(value.Name ?? string.Empty);
        

        output.Write(value.Transform.HasValue);
        if (value.Transform.HasValue)
        {
            var transform = value.Transform.Value;
            output.Write(transform.Position);
            output.Write(transform.Rotation);
            output.Write(transform.Scale);
        }

        output.Write(value.Velocity.HasValue);
        if (value.Velocity.HasValue)
        {
            var velocity = value.Velocity.Value;
            output.Write(velocity.Value);
        }

        output.Write(value.Sprite.HasValue);
        if (value.Sprite.HasValue)
        {
            var sprite = value.Sprite.Value;
            output.Write(sprite.Texture);
            output.Write(sprite.Color);
            output.Write(sprite.IsEquipped);
        }

        output.Write(value.PlayerControlled);
    }
    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return
            "HeartThrobFramework.Utils.EntityTemplateReader, HeartThrobFramework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}