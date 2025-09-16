using HeartThrobFramework.GameData.Template;
using HeartThrobFramework.Utils;
using Microsoft.Xna.Framework.Content;

namespace HeartThrobFramework.Core.ECS
{
    internal class TemplateManager(ContentManager content)
    {
        Dictionary<string, EntityTemplate> Templates = new Dictionary<string, EntityTemplate>();

        public void LoadAllTemplates(ContentManager content)
        {
            var templateNames = EntityTemplateNames.TemplateNames();

            foreach (var templateName in templateNames)
            {
                EntityTemplate template = content.Load<EntityTemplate>($"Entities/{templateName}");

                Templates.Add(templateName, template);
            }

        }

        public EntityTemplate GetTemplate(string templateName)
        {
            Templates.TryGetValue(templateName, out var template);

            return template;
        }
    }
}
