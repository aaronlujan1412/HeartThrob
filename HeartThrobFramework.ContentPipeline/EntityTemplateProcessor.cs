using System.Text.Json;
using HeartThrobFramework.Utils;
using Microsoft.Xna.Framework.Content.Pipeline;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Serialization.Json;
using Newtonsoft.Json;

namespace HeartThrobFramework.ContentPipeline;

[ContentProcessor(DisplayName = "EntityTemplateProcessor")]
class EntityTemplateProcessor : ContentProcessor<string, EntityTemplate>
{
    public override EntityTemplate Process(string input, ContentProcessorContext context)
    {
        var options = new JsonSerializerOptions();

        options.Converters.Add(new ColorJsonConverter());
        options.Converters.Add(new Vector2JsonConverter());

        EntityTemplate template = System.Text.Json.JsonSerializer.Deserialize<EntityTemplate>(input, options);

        return template;
        
        
        
        // EntityTemplate newEntityTemplate = JsonConvert.DeserializeObject<EntityTemplate>(input,
        //     new Vector2JsonConverter(),
        //     new ColorJsonConverter());
        //
        // return newEntityTemplate;
    }
}