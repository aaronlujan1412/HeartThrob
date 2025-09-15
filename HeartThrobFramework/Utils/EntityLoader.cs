using HeartThrobFramework.GameData.Template;
using System.IO;
using System.Text.Json;

namespace HeartThrobFramework.Utils;

public static class EntityLoader
{
    public static EntityTemplate LoadEntity(string filename)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "Content", filename);
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<EntityTemplate>(json)!;
    }
}