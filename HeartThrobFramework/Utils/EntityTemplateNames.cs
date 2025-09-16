using System.Reflection;

namespace HeartThrobFramework.Utils
{
    public static class EntityTemplateNames
    {
        public const string Slime = "slime";
        public const string Pause = "pause";


        public static string[] TemplateNames()
        {
            var result = typeof(EntityTemplateNames)
                .GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(f => f.IsLiteral && f.FieldType == typeof(string))
                .Select(f => (string)f.GetValue(null));

            return [.. result];
        }
    }
}
