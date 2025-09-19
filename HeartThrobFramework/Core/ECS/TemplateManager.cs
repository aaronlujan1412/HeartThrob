using HeartThrobFramework.GameData.Template;
using HeartThrobFramework.Utils;
using Microsoft.Xna.Framework.Content;

namespace HeartThrobFramework.Core.ECS
{
    public class TemplateManager()
    {
        private readonly Dictionary<string, EntityTemplate> _templates = [];

        public void LoadAllTemplates(ContentManager content)
        {
            var templateNames = EntityTemplateNames.TemplateNames();

            foreach (var templateName in templateNames)
            {
                EntityTemplate template = content.Load<EntityTemplate>($"Entities/{templateName}");

                _templates.Add(templateName, template);
            }

        }

        public EntityTemplate GetTemplate(string templateName)
        {
            if (!_templates.TryGetValue(templateName, out var template))
            {
                throw new InvalidOperationException($"Template with name {templateName} does not exist in current context.");
            }

            return template;
        }
    }
}
