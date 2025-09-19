using HeartThrobFramework.Components;
using System.Reflection;

namespace HeartThrobFramework.Utils
{
    public static class EcsUtils
    {
        public static IEnumerable<Type> GetAllComponentTypes(Assembly assembly)
        {
            var componentInterface = typeof(IComponent);

            try
            {
                return assembly.GetTypes()
                    .Where(t => t.IsValueType && componentInterface.IsAssignableFrom(t));
            }
            catch (ReflectionTypeLoadException)
            {
                return Enumerable.Empty<Type>();
            }
        }
    }
}
