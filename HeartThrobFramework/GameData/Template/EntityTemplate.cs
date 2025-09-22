using HeartThrobFramework.GameData.ComponentDatas;

namespace HeartThrobFramework.GameData.Template;

public class EntityTemplate
{
    public string Name { get; set; }
    public string? Inherits { get; set; }
    public TransformData? Transform { get; set; }
    public VelocityData? Velocity { get; set; }
    public SpriteData? Sprite { get; set; }
    public bool PlayerControlled { get; set; }
    public bool Clickable { get; set; }
}