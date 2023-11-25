using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using HarmonyLib;

using Kingmaker.Controllers.Rest.State;

using UnityModManagerNet;

namespace DefaultRations
{
    public static class Main
    {
        internal static UnityModManager.ModEntry Mod;

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            Mod = modEntry;

            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            return true;
        }

        [HarmonyPatch(typeof(CampingState), MethodType.Constructor)]
        public static class CampingState_ctor_Patch
        {
            [SuppressMessage("Style", "IDE1006")]
            public static void Postfix(CampingState __instance)
            {
                // The variable behind this getter is not manually initialized, and being a bool,
                // that makes it `false`. Overriding it here at the end of the constructor is
                // sufficient to invert the default behavior, while leaving the persistence intact
                // for all the different toggles that effect this property.
                __instance.UseRationsFromStash = true;
            }
        }
    }
}
