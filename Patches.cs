using Harmony;
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
        #region TorpedoType Patch
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
        #endregion
    }

    #region allowedTech Patch
    [HarmonyPatch(typeof(ItemsContainer))]
    [HarmonyPatch("IsTechTypeAllowed")]

    public class AllowedTechPatch
    {
        
    }
            /*
        // Possible Method: So there's a TechType called "SeamothTorpedoModule" that seems to be a subtype of ItemsContainer
        // So if you can run a check to see if a container = SeamothTorpedoModule on access, you can patch in 
        // your torpedos as allowed tech types.
                                                     or...
           A little prefix that sets IsTechTypeAllowed to true when you hover over one of my torpedoes.
        

        //Probably the best funtion to patch "IsTechTypeAllowed" from ItemsContainer
            private bool IsTechTypeAllowed(TechType techType)
            {
            return this.allowedTech == null || this.allowedTech.Count == 0 || this.allowedTech.Contains(techType);
            }


            // ItemsContainer "Allowed tech" from dnSpy - Assembly-CSharp.dll
            public void SetAllowedTechTypes(TechType[] allowedTech)
            {
                if (this.allowedTech == null)
                {
                    this.allowedTech = new HashSet<TechType>();
                }
                else
                {
                    this.allowedTech.Clear();
                }
                for (int i = 0; i < allowedTech.Length; i++)
                {
                    this.allowedTech.Add(allowedTech[i]);
                }
            }
            */
}
        #endregion