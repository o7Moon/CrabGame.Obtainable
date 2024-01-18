using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using UnityEngine;

namespace obtainable
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        static bool inUseItemHook = false;
        public override void Load()
        {
            Harmony.CreateAndPatchAll(typeof(Plugin));

            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        [HarmonyPatch(typeof(ServerHandle), nameof(ServerHandle.UseItem))]
        [HarmonyPrefix]
        public static void useItemHookPre(ulong __0, Packet __1) {
            inUseItemHook = true;
        }
        [HarmonyPatch(typeof(ServerHandle), nameof(ServerHandle.UseItem))]
        [HarmonyPostfix]
        public static void useItemHookPost(ulong __0, Packet __1) {
            inUseItemHook = false;
        }
        [HarmonyPatch(typeof(ServerHandle), nameof(ServerHandle.CrabDamage))]
        [HarmonyPrefix]
        public static void useItemHookPre2(ulong __0, Packet __1) {
            inUseItemHook = true;
        }
        [HarmonyPatch(typeof(ServerHandle), nameof(ServerHandle.CrabDamage))]
        [HarmonyPostfix]
        public static void useItemHookPost2(ulong __0, Packet __1) {
            inUseItemHook = false;
        }
        [HarmonyPatch(typeof(ServerHandle), nameof(ServerHandle.PlayerDamage))]
        [HarmonyPrefix]
        public static void useItemHookPre3(ulong __0, Packet __1) {
            inUseItemHook = true;
        }
        [HarmonyPatch(typeof(ServerHandle), nameof(ServerHandle.PlayerDamage))]
        [HarmonyPostfix]
        public static void useItemHookPost3(ulong __0, Packet __1) {
            inUseItemHook = false;
        }
        [HarmonyPatch(typeof(LobbyManager), nameof(LobbyManager.BanPlayer))]
        [HarmonyPrefix]
        public static bool BanPlayerHook(ulong __0) {
            if (inUseItemHook) return false;
            else return true;
        }
    }
}