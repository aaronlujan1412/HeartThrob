using System.Text.Json.Serialization;

namespace HeartThrobFramework.GameData.Template.AnimationSet
{
    public record MetaDataTemplate(
        [property: JsonPropertyName("app")] string App,
        [property: JsonPropertyName("version")] string Version,
        [property: JsonPropertyName("image")] string Image,
        [property: JsonPropertyName("format")] string Format,
        [property: JsonPropertyName("size")] Size Size,
        [property: JsonPropertyName("scale")] string Scale,
        [property: JsonPropertyName("frameTags")] FrameTag[] FrameTags,
        [property: JsonPropertyName("layers")] Layer[] Layers,
        [property: JsonPropertyName("slices")] Slice[]? Slices = null
        );

    public record FrameTag(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("from")] int From,
        [property: JsonPropertyName("to")] int To,
        [property: JsonPropertyName("direction")] string Direction,
        [property: JsonPropertyName("color")] string? ColorHex = null
        );

    public record Layer(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("opacity")] int Opacity,
        [property: JsonPropertyName("blendMode")] string BlendMode,
        [property: JsonPropertyName("cels")] Cel[]? Cels = null
        );

    public record Cel(
        [property: JsonPropertyName("frame")] int Frame,
        [property: JsonPropertyName("zIndex")] int? ZIndex
        );

    public record Size(
        [property: JsonPropertyName("w")] int W,
        [property: JsonPropertyName("h")] int H
        );

    public record Slice(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("color")] string? Color,
        [property: JsonPropertyName("keys")] Key[] Keys
        );

    public record Key(
        [property: JsonPropertyName("frame")] int Frame,
        [property: JsonPropertyName("bounds")] Boundary Bounds,
        [property: JsonPropertyName("pivot")] Pivot? Pivot = null
        );

    public record Boundary(
        [property: JsonPropertyName("x")] int X,
        [property: JsonPropertyName("y")] int Y,
        [property: JsonPropertyName("w")] int W,
        [property: JsonPropertyName("h")] int H
        );

    public record Pivot(
        [property: JsonPropertyName("x")] int X,
        [property: JsonPropertyName("y")] int Y
        );
}
