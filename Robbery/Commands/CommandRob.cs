using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;

namespace Robbery.Commands
{
    public class CommandRob : Command
    {
        protected override void execute(CSteamID executorID, string parameter)
        {
            Player ply = PlayerTool.getPlayer(executorID);

            Items virtualInventory = new Items(7);

            virtualInventory.resize((byte)10, (byte)10);

            List<Player> foundPlayers = new List<Player>();

            PlayerTool.getPlayersInRadius(ply.transform.position, 10f, foundPlayers);

            if (foundPlayers.Count <= 1)
            {
                return;
            }

            if (foundPlayers.Contains(ply))
                foundPlayers.Remove(ply);

            Player robee = foundPlayers.ToArray()[0];

            if (robee is null)
            {
                return;
            }

            if (robee.animator.gesture != EPlayerGesture.ARREST_START && robee.animator.gesture != EPlayerGesture.SURRENDER_START)
            {
                return;
            }

            foreach (Items page in robee.inventory.items)
            {
                if (page != null)
                {
                    foreach (ItemJar item in page.items)
                    {
                        if (item != null)
                        {
                            ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.item.id);

                            virtualInventory.tryAddItem(item.item, true);
                        }
                    }
                }
            }

            virtualInventory.onItemAdded = (byte page, byte index, ItemJar item) => {
                /*
                Console.WriteLine("-----Item Added-----");
                Console.WriteLine($"Page: {page}");
                Console.WriteLine($"Index: {index}");
                Console.WriteLine($"ID: {item.item.id}");
                Console.WriteLine($"Y: {item.x}");
                Console.WriteLine($"X: {item.y}");
                */

                robee.inventory.tryAddItem(item.item, true);
            };

            virtualInventory.onItemRemoved = (byte page, byte index, ItemJar item) =>
            {
                /*
                Console.WriteLine("-----Item Removed-----");
                Console.WriteLine($"Page: {page}");
                Console.WriteLine($"Index: {index}");
                Console.WriteLine($"ID: {item.item.id}");
                Console.WriteLine($"Y: {item.x}");
                Console.WriteLine($"X: {item.y}");
                */

                List<InventorySearch> search = new List<InventorySearch>();

                foreach (Items invPage in robee.inventory.items)
                {
                    if (invPage != null)
                    {
                        foreach (ItemJar invItem in invPage.items)
                        {
                            if (invItem != null)
                            {
                                search.Add(new InventorySearch(invPage.page, invItem));
                            }
                        }
                    }
                }

                List<InventorySearch> result = robee.inventory.search(search);

                foreach (InventorySearch r in result)
                {
                    /*
                        Console.WriteLine("-----Search Result-----");
                        Console.WriteLine($"Page: {r.page}");
                        Console.WriteLine($"Index: {robee.inventory.getIndex(r.page, r.jar.x, r.jar.y)}");
                        Console.WriteLine($"ID: {r.jar.item.id}");
                        Console.WriteLine($"Y: {r.jar.x}");
                        Console.WriteLine($"X: {r.jar.y}");
                        Console.WriteLine($"Amount: {r.jar.item.amount}");
                    */

                    robee.inventory.removeItem(r.page, robee.inventory.getIndex(r.page, r.jar.x, r.jar.y));
                }
            };

            ply.inventory.updateItems(7, virtualInventory);

            ply.inventory.sendStorage();










            foreach (Items page in robee.inventory.items)
            {
                if (page != null)
                {
                    foreach (ItemJar item in page.items)
                    {
                        if (item != null)
                        {
                            
                            ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, item.item.id);

                            virtualInventory.tryAddItem(item.item, true);
                        }
                    }
                }
            }
        }

        public CommandRob()
        {
            this.localization = new Local();
            this._command = "rob";
            this._info = "rob";
            this._help = "Rob a player";
        }
    }
}
