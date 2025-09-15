using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace HeartThrobFramework.ContentPipeline;

[ContentImporter(".json", DisplayName = "JsonImporter", DefaultProcessor = "EntityTemplateProcessor")]
public class JsonImporter : ContentImporter<string>
{
    public override string Import(string filename, ContentImporterContext context)
    {
        return File.ReadAllText(filename);
    }
}