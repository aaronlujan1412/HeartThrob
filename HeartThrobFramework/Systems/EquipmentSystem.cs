using HeartThrobFramework.Components;
using Microsoft.Xna.Framework.Graphics;
using HeartThrobFramework.Core;
using Microsoft.Xna.Framework;

namespace HeartThrobFramework.Systems;

public class EquipmentSystem : ISystem
{
    public void Update(World world, float deltaTime)
    {
        var playerEntities = world.Query<TransformComponent, PlayerControlledComponent>();
        var itemEntities = world.Query<EquipmentComponent>();

        foreach (int playerId in playerEntities)
        {
            foreach (int itemId in itemEntities)
            {
                var playerPosition = world.GetComponent<TransformComponent>(playerId);
                var itemPosition = world.GetComponent<TransformComponent>(itemId);

                if (Vector2.Distance(playerPosition.Position, itemPosition.Position) < 0.1f)
                {
                    var inventory = world.GetComponent<InventoryComponent>(playerId);
                    
                    inventory.items.Add(itemId);

                    world.RemoveComponent<TransformComponent>(itemId);
                    world.RemoveComponent<SpriteComponent>(itemId);
                }
            }
        }
    }

    public void Render(World world, SpriteBatch spriteBatch)
    {
        
    }
}