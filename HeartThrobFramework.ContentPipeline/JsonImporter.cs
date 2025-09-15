using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using TImport = System.String;

namespace HeartThrobFramework.ContentPipeline;

[ContentImporter(".json", DisplayName = "JsonImporter", DefaultProcessor = "EntityTemplateProcessor")]
public class JsonImporter : ContentImporter<TImport>
{
    public override TImport Import(string filename, ContentImporterContext context)
    {
        return File.ReadAllText(filename);
    }
}