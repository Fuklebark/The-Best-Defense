namespace TheBestDefense
{
    using System.Collections.Generic;
    using SMLHelper;
    using SMLHelper.Patchers;
    using Harmony;
    using UnityEngine;
    using Object = UnityEngine.Object;
    using System.Reflection;

    public class Main
    {
        #region Definitions
        public static TechType ConcussionTorpedoTechType;
        public const string ConcussionNameID = "ConcussionTorpedo";
        public const string ConcussionFriendlyName = "Concussion Torpedo";
        public const string ConcussionDescription = "A standard explosive payload optimized for underwater use. Great for parties.";

        public static TechType NukeTorpedoTechType;
        public const string NukeNameID = "NukeTorpedo";
        public const string NukeFriendlyName = "MicroNuke Torpedo";
        public const string NukeDescription = "A highly miniaturized  clean fission warhead. Landscaping or sports use discouraged.";

        public static void Patch()
        {
            #region Harmony Patch
            HarmonyInstance harmony = HarmonyInstance.Create("com.Fuklebark.TheBestDefense");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            #endregion

            var assetBundle = AssetBundle.LoadFromFile(@"./QMods/TheBestDefense/TheBestDefense.assets");

            // Create a new TechType
            ConcussionTorpedoTechType = TechTypePatcher.AddTechType(ConcussionNameID, ConcussionFriendlyName, ConcussionDescription, unlockOnGameStart: true);
            NukeTorpedoTechType = TechTypePatcher.AddTechType(NukeNameID, NukeFriendlyName, NukeDescription, unlockOnGameStart: true);

            // Create the in-game item
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(ConcussionNameID, $"WorldEntities/Tools/{ConcussionNameID}", ConcussionTorpedoTechType, GetConcussionTorpedoObject));
            CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(NukeNameID, $"WorldEntities/Tools/{NukeNameID}", NukeTorpedoTechType, GetNukeTorpedoObject));

            // Get the custom icon from the Unity assets bundle
            CustomSpriteHandler.customSprites.Add(new CustomSprite(ConcussionTorpedoTechType, assetBundle.LoadAsset<Sprite>("ConcussionTorpedo")));
            CustomSpriteHandler.customSprites.Add(new CustomSprite(NukeTorpedoTechType, assetBundle.LoadAsset<Sprite>("NukeTorpedo")));

            // Add the new recipe to the Vehicle Station crafting tree
            CraftTreePatcher.customNodes.Add(new CustomCraftNode(ConcussionTorpedoTechType, CraftTree.Type.SeamothUpgrades, $"TorpedoesMenu /{ConcussionNameID}"));
            CraftTreePatcher.customNodes.Add(new CustomCraftNode(NukeTorpedoTechType, CraftTree.Type.SeamothUpgrades, $"TorpedoesMenu /{NukeNameID}"));

            // Create a new Recipie and pair the new recipie with the new TechType
            CraftDataPatcher.customTechData[ConcussionTorpedoTechType] = GetConcussionRecipe();
            CraftDataPatcher.customTechData[NukeTorpedoTechType] = GetNukeRecipe();

            // Define it's Equipment Type (Can't find an obvious one, There's gotta be a way it knows how to distingish a torpedo for the bay, but I dunno how)
            //CraftDataPatcher.customEquipmentTypes[ConcussionTorpedoTechType] = EquipmentType.SeamothTorpedo;
            //CraftDataPatcher.customEquipmentTypes[NukeTorpedoTechType] = EquipmentType.SeamothTorpedo;
        }
        #endregion

        #region Recipies
        //    _craftAmount = 1,
        //    _ingredients = new List<IngredientHelper>(new IngredientHelper[4]
        //                 {
        //                 new IngredientHelper(TechType.SeamothSolarCharge, 1), // This is to make sure the player has access to vehicle solar charging
        //                 new IngredientHelper(TechType.Quartz, 3),
        //                 new IngredientHelper(TechType.Titanium, 3),
        //                 new IngredientHelper(TechType.CopperWire, 1),
        //                 }),
        //    _techType = CySolarChargerTechType
        //};

        public static TechDataHelper GetConcussionRecipe()
        {
            return new TechDataHelper()
            {
                _craftAmount = 2,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[3]
                {
                    new IngredientHelper(TechType.Titanium, 1),
                    new IngredientHelper(TechType.CrashPowder, 1),
                    new IngredientHelper(TechType.Salt, 1)
                }),
                _techType = ConcussionTorpedoTechType
            };
        }

        public static TechDataHelper GetNukeRecipe()
        {
            return new TechDataHelper()
            {
                _craftAmount = 2,
                _ingredients = new List<IngredientHelper>(new IngredientHelper[3]
                    {
                        new IngredientHelper(TechType.Titanium, 2),
                        new IngredientHelper(TechType.UraniniteCrystal, 4),
                        new IngredientHelper(TechType.Lead, 2)
                    }),
                _techType = NukeTorpedoTechType,
            };
        }
        #endregion

        #region Prefabs
        //Copies and modifies the gastorpedo PreFab to represent the new torpedo types (I think?)
        public static GameObject GetConcussionTorpedoObject()
        {
            var prefab = Resources.Load<GameObject>("WorldEntities/Tools/WhirlpoolTorpedo");
            var obj = GameObject.Instantiate(prefab);

            var identifier = obj.GetComponent<PrefabIdentifier>();
            var techTag = obj.GetComponent<TechTag>();

            identifier.ClassId = ConcussionNameID;
            techTag.type = ConcussionTorpedoTechType;

            return obj;
        }

        public static GameObject GetNukeTorpedoObject()
        {
            var prefab = Resources.Load<GameObject>("WorldEntities/Tools/WhirlpoolTorpedo");
            var obj = GameObject.Instantiate(prefab);

            var identifier = obj.GetComponent<PrefabIdentifier>();
            var techTag = obj.GetComponent<TechTag>();

            identifier.ClassId = NukeNameID;
            techTag.type = NukeTorpedoTechType;

            TorpedoType NukeTorpedoType = new TorpedoType();
            this.torpedoType.techType = NukeTorpedoTechType;
            this.torpedoType.prefab = myPrefab;
            
return obj;

        }
        #endregion
    }

    #region TorpedoType Patcher
    [HarmonyPatch(typeof(Vehicle))]
    [HarmonyPatch("Awake")]

    public class MyHarmonyPatches
    {
        TorpedoType ConcussionTorpedoType = new TorpedoType()
        ;
        TorpedoType NukeTorpedoType = new TorpedoType(Main.NukeTorpedoTechType, Main.NukeNameID);

        static void MyVehiclePostfix(Vehicle __instance)
        {
            __instance.torpedoTypes.Add(Main.ConcussionTorpedoType);
            __instance.torpedoTypes.Add(NukeTorpedoType)
        };         
            
        /*
          TorpedoType Definition from dnSpy - Assembly-CSharp.dll
            [Serializable]
            public class TorpedoType
        {
            // Token: 0x040036D7 RID: 14039
            public TechType techType;

            // Token: 0x040036D8 RID: 14040
            public GameObject prefab;
        }
        */
    }

}