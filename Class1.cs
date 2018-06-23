namespace The_Best_Defense
{
    using System.Collections.Generic;
    using SMLHelper;
    using SMLHelper.Patchers;
    using UnityEngine;
    using Object = UnityEngine.Object;

        public class Main
        {
            public static TechType ConcussionTorpedoTechType;
            public const string ConcussionNameID = "ConcussionTorpedo";
            public const string ConcussionFriendlyName = "Concussion Torpedo";
            public const string ConcussionDescription = "A standard explosive payload optimized for underwater use";

            public static TechType NukeTorpedoTechType;
            public const string NukeNameID = "NukeTorpedo";
            public const string NukeFriendlyName = "MicroNuke Torpedo";
            public const string NukeDescription = "A highly miniaturized and clean fission warhead. Landscaping or sports use discouraged.";

        public static void Patch(AssetBundle assetBundle)
            {
                // Create a new TechType
                ConcussionTorpedoTechType = TechTypePatcher.AddTechType(ConcussionNameID, ConcussionFriendlyName, ConcussionDescription, unlockOnGameStart: true);
                NukeTorpedoTechType = TechTypePatcher.AddTechType(NukeNameID, NukeFriendlyName, NukeDescription, unlockOnGameStart: true);

                // Create the in-game item that will behave like any other Cyclops upgrade module
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(ConcussionNameID, $"WorldEntities/Tools/{ConcussionNameID}", ConcussionTorpedoTechType, GetConcussionTorpedoObject));
                CustomPrefabHandler.customPrefabs.Add(new CustomPrefab(NukeNameID, $"WorldEntities/Tools/{NukeNameID}", NukeTorpedoTechType, GetNukeTorpedoObject));

                // Get the custom icon from the Unity assets bundle
                CustomSpriteHandler.customSprites.Add(new CustomSprite(ConcussionTorpedoTechType, assetBundle.LoadAsset<Sprite>("ConcussionTorpedo")));
                CustomSpriteHandler.customSprites.Add(new CustomSprite(ConcussionTorpedoTechType, assetBundle.LoadAsset<Sprite>("NukeTorpedo")));

                // Add the new recipe to the Vehicle Station crafting tree
                CraftTreePatcher.customNodes.Add(new CustomCraftNode(ConcussionTorpedoTechType, CraftTree.Type.SeamothUpgrades, $"TorpedoesMenu /{ConcussionNameID}"));
                CraftTreePatcher.customNodes.Add(new CustomCraftNode(NukeTorpedoTechType, CraftTree.Type.SeamothUpgrades, $"TorpedoesMenu /{NukeNameID}"));

                // Create a new Recipie and pair the new recipie with the new TechType
                CraftDataPatcher.customTechData[ConcussionTorpedoTechType] = GetConcussionRecipe();
                CraftDataPatcher.customTechData[NukeTorpedoTechType] = GetNukeRecipe();

                // Define it's Equipment Type
                CraftDataPatcher.customEquipmentTypes[ConcussionTorpedo] = EquipmentType.SeamothTorpedo;
                CraftDataPatcher.customEquipmentTypes[NukeTorpedo] = EquipmentType.SeamothTorpedo;
            }
        }

    public class Recipies
    { 
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

        private static TechDataHelper GetConcussionRecipe()
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

        private static TechDataHelper GetNukeRecipe()
        {
            return new TechDataHelper()
            {
                _craftAmount = 2,
                _ingredients = new List<IngredientHelper>()
                {
                    new IngredientHelper(TechType.Titanium, 2),
                    new IngredientHelper(TechType.UraniteCrystal, 2),
                    new IngredientHelper(TechType.Lead, 2)
                },
                _techType = NukeTorpedoTechType,
            };
        }
        
        //Copies and modifies the gastorpedo PreFab to represent the new torpedo types (I think?)
        public static GameObject GetConcussionTorpedoObject()
        { 
            GameObject prefab = Resources.Load<GameObject>("WorldEntities/Tools/gastorpedo");
            GameObject obj = Object.Instantiate(prefab);

            obj.GetComponent<PrefabIdentifier>().ClassId = ConcussionTorpedo;
            obj.GetComponent<TechTag>().type = ConcussionTorpedoTechType;

            return obj;
        }

        public static GameObject GetNukeTorpedoObject()
        {
            GameObject prefab = Resources.Load<GameObject>("WorldEntities/Tools/gastorpedo");
            GameObject obj = Object.Instantiate(prefab);

            obj.GetComponent<PrefabIdentifier>().ClassId = NukeTorpedo;
            obj.GetComponent<TechTag>().type = NukeTorpedoTechType;

            return obj;
        }
    }
}