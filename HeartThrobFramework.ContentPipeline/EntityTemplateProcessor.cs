using System.Text.Json;
using HeartThrobFramework.Utils;
using Microsoft.Xna.Framework.Content.Pipeline;
using MonoGame.Extended.Serialization.Json;

namespace HeartThrobFramework.ContentPipeline;

[ContentProcessor(DisplayName = "EntityTemplateProcessor")]
class EntityTemplateProcessor : ContentProcessor<string, EntityTemplate>
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters =
        {
            new ColorJsonConverter(),
            new Vector2JsonConverter(),
            new RectangleFJsonConverter()
        }
    };

    public override EntityTemplate Process(string input, ContentProcessorContext context)
    {
        return JsonSerializer.Deserialize<EntityTemplate>(input, _jsonOptions);
    }
}