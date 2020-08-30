using Robbery.Utils;
using SDG.Framework.Modules;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Robbery
{
    public class Main : MonoBehaviour, IModuleNexus
    {
        private static GameObject RobberObject;

        public static Main Instance;

        public static Config Config;

        public void initialize()
        {
            Instance = this;
            Console.WriteLine("Robbery by Corbyn loaded");

            RobberObject = new GameObject("Robbery");
            DontDestroyOnLoad(RobberObject);

            /*
            string callingPath = $"{Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)}";

            DirectoryInfo configDirectory = Directory.CreateDirectory($"{callingPath}/config");

            ConfigHelper.EnsureConfig($"{callingPath}/config/robbery.json");

            Config = ConfigHelper.ReadConfig($"{callingPath}/config/robbery.json");
            */

            RobberObject.AddComponent<RobberyManager>();
        }


        public void shutdown()
        {
            Instance = null;
        }
    }
}
