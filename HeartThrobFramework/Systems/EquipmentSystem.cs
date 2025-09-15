using HeartThrobFramework.Components;
using Microsoft.Xna.Framework.Graphics;
using HeartThrobFramework.Core;
using Microsoft.Xna.Framework;

namespace HeartThrobFramework.Systems;

public class EquipmentSystem : ISystem
{
    public void Update(World world, float deltaTime)
    {
        var playerEntities = world.Query<Transform, PlayerControlled>();
        var itemEntities = world.Query<Equipment>();

        foreach (int playerId in playerEntities)
        {
            foreach (int itemId in itemEntities)
            {
                var playerPosition = world.GetComponent<Transform>(playerId);
                var itemPosition = world.GetComponent<Transform>(itemId);

                if (Vector2.Distance(playerPosition.Position, itemPosition.Position) < 0.1f)
                {
                    var inventory = world.GetComponent<Inventory>(playerId);
                    
                    inventory.items.Add(itemId);

                    world.RemoveComponent<Transform>(itemId);
                    world.RemoveComponent<Sprite>(itemId);
                }
            }
        }
    }

    public void Render(World world, SpriteBatch spriteBatch)
    {
        
    }
}