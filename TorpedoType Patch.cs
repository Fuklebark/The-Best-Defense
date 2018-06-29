using Harmony;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheBestDefense
{
    [HarmonyPatch(typeof(Vehicle))]
    [HarmonyPatch("Awake")]

    public class TorpedoTypePatch
    {
        public static TorpedoType ConcussionTorpedoType = new TorpedoType()
        {
            techType = Main.ConcussionTorpedoTechType,
            prefab = Main.GetConcussionTorpedoObject()
        };

        public static TorpedoType NukeTorpedoType = new TorpedoType()
        {
            techType = Main.NukeTorpedoTechType,
            prefab = Main.GetNukeTorpedoObject()
        };

        static void Postfix(Vehicle __instance)
        {
            __instance.torpedoTypes.Add(ConcussionTorpedoType);
            __instance.torpedoTypes.Add(NukeTorpedoType);
        }
    }
}