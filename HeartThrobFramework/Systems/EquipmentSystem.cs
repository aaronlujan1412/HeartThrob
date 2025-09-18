using HeartThrobFramework.Components;
using Microsoft.Xna.Framework.Graphics;
using HeartThrobFramework.Core;
using Microsoft.Xna.Framework;

namespace HeartThrobFramework.Systems;

public class EquipmentSystem : ISystem
{
    public World World { get; set; }
    public void Update(float deltaTime)
    {
        var playerEntities = World.Query<TransformComponent, PlayerControlledComponent>();
        var itemEntities = World.Query<EquipmentComponent>();

        foreach (int playerId in playerEntities)
        {
            foreach (int itemId in itemEntities)
            {
                var playerPosition = World.GetComponent<TransformComponent>(playerId);
                var itemPosition = World.GetComponent<TransformComponent>(itemId);

                if (Vector2.Distance(playerPosition.Position, itemPosition.Position) < 0.1f)
                {
                    var inventory = World.GetComponent<InventoryComponent>(playerId);
                    
                    inventory.Items.Add(itemId);

                    World.RemoveComponent<TransformComponent>(itemId);
                    World.RemoveComponent<SpriteComponent>(itemId);
                }
            }
        }
    }

    public void Render(SpriteBatch spriteBatch)
    {
        
    }
}