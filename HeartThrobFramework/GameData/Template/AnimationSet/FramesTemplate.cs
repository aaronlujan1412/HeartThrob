using System.Text.Json.Serialization;

namespace HeartThrobFramework.GameData.Template.AnimationSet
{
    public record FrameData(
        [property: JsonPropertyName("frame")] FrameRectData FrameRect,
        [property: JsonPropertyName("rotated")] bool Rotated,
        [property: JsonPropertyName("trimmed")] bool Trimmed,
        [property: JsonPropertyName("spriteSourceSize")] SpriteSourceSize SpriteSize,
        [property: JsonPropertyName("sourceSize")] SourceSize Size,
        [property: JsonPropertyName("duration")] int Duration
        );

    public record FrameRectData(
        [property: JsonPropertyName("x")] int X,
        [property: JsonPropertyName("y")] int Y,
        [property: JsonPropertyName("w")] int W,
        [property: JsonPropertyName("h")] int H
        );

    public record SpriteSourceSize(
        [property: JsonPropertyName("x")] int X,
        [property: JsonPropertyName("y")] int Y,
        [property: JsonPropertyName("w")] int W,
        [property: JsonPropertyName("h")] int H
        );

    public record SourceSize(
        [property: JsonPropertyName("w")] int W,
        [property: JsonPropertyName("h")] int H
        );
}
