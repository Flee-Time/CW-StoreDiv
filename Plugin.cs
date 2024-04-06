using BepInEx;
using BepInEx.Configuration;
using Zorro.Core;
using UnityEngine;

namespace StoreDiv
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {

        static internal ConfigFile? StoreDivConfigFile;

        private void Awake()
        {
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Logger.LogInfo("Config file is loaded!");

            StoreDivConfigFile = base.Config;
            StoreDivConfigFile?.Bind("Settings", "PriceDivisionAmount", 2, "The division amount of the current prices."); // This creates the config file if it doesn't exist

            ItemDatabase itemDatabase = SingletonAsset<ItemDatabase>.Instance;
            foreach (Item item in itemDatabase.Objects)
            {
                if (item.purchasable)
                {
                    item.price = Mathf.RoundToInt(item.price / (int)StoreDivConfigFile?.Bind("Settings", "PriceDivisionAmount", 2, "The division amount of the current prices.").Value); // This creates the config file if it doesn't exist
                }
            }
        }
    }
}
