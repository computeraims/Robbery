using Robbery.Commands;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Robbery
{
    public class RobberyManager : MonoBehaviour
    {
        public static Dictionary<CSteamID, CSteamID> robbedPlayers;
        public void Awake()
        {
            Console.WriteLine("RobberyManager loaded");

            Commander.register(new CommandRob());

            ChatManager.onCheckPermissions += OnCheckedPermissions;
        }

        private void OnCheckedPermissions(SteamPlayer player, string text, ref bool shouldExecuteCommand, ref bool shouldList)
        {
            if (text.StartsWith("/rob"))
            {
                shouldExecuteCommand = true;
            }
        }
    }
}
