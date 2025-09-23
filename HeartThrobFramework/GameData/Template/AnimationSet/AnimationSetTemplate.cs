using System.Text.Json.Serialization;

namespace HeartThrobFramework.GameData.Template.AnimationSet
{
    public record AnimationSetTemplate(
        [property: JsonPropertyName("frames")] Dictionary<string, FrameData> Frames,
        [property: JsonPropertyName("meta")] MetaDataTemplate Metadata
        );
}
