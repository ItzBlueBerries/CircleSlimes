using HarmonyLib;
using SRML;
using SRML.SR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using static ShortcutLib.Shortcut;

namespace CircleSlimes
{
    [HarmonyPatch(typeof(SRModLoader), "PostLoadMods")]
    internal static class PostLoadMod
    {
        internal static AssetBundle circle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(Main), "circlemod"));
        public static void Postfix()
        {
            foreach (Identifiable.Id id in Identifiable.SLIME_CLASS)
            {
                SlimeAppearance.SlimeBone[] attachedBones = new SlimeAppearance.SlimeBone[]
                {
                    SlimeAppearance.SlimeBone.JiggleBack,
                    SlimeAppearance.SlimeBone.JiggleBottom,
                    SlimeAppearance.SlimeBone.JiggleFront,
                    SlimeAppearance.SlimeBone.JiggleLeft,
                    SlimeAppearance.SlimeBone.JiggleRight,
                    SlimeAppearance.SlimeBone.JiggleTop
                };

                if (Prefab.GetPrefab(id) != null && Slime.GetSlimeDef(id).AppearancesDefault[0] != null)
                {
                    (GameObject, SlimeAppearanceObject, SlimeAppearance.SlimeBone[]) circleBody = Structure.CreateBasicStructure(circle, "circle", "slime_circle_body", SlimeAppearance.SlimeBone.Slime, SlimeAppearance.SlimeBone.None, attachedBones, RubberBoneEffect.RubberType.Slime, true);
                    circleBody.Item2.IgnoreLODIndex = true;
                    AssetsLib.MeshUtils.GenerateBoneData(Prefab.GetPrefab(id).GetComponent<SlimeAppearanceApplicator>(), circleBody.Item2, 0.15f);
                    Structure.SetStructureElement(Slime.GetSlimeDef(id).AppearancesDefault[0], new SlimeAppearanceObject[] { circleBody.Item2 }, 0, false, true);
                }
            }

            foreach (Identifiable.Id id in Identifiable.LARGO_CLASS)
            {
                SlimeAppearance.SlimeBone[] attachedBones = new SlimeAppearance.SlimeBone[]
                {
                    SlimeAppearance.SlimeBone.JiggleBack,
                    SlimeAppearance.SlimeBone.JiggleBottom,
                    SlimeAppearance.SlimeBone.JiggleFront,
                    SlimeAppearance.SlimeBone.JiggleLeft,
                    SlimeAppearance.SlimeBone.JiggleRight,
                    SlimeAppearance.SlimeBone.JiggleTop
                };

                if (Prefab.GetPrefab(id) != null && Slime.GetSlimeDef(id).AppearancesDefault[0] != null)
                {
                    (GameObject, SlimeAppearanceObject, SlimeAppearance.SlimeBone[]) circleBody = Structure.CreateBasicStructure(circle, "circle", "slime_circle_body", SlimeAppearance.SlimeBone.Slime, SlimeAppearance.SlimeBone.None, attachedBones, RubberBoneEffect.RubberType.Slime, true);
                    circleBody.Item2.IgnoreLODIndex = true;
                    AssetsLib.MeshUtils.GenerateBoneData(Prefab.GetPrefab(id).GetComponent<SlimeAppearanceApplicator>(), circleBody.Item2, 0.15f);
                    Structure.SetStructureElement(Slime.GetSlimeDef(id).AppearancesDefault[0], new SlimeAppearanceObject[] { circleBody.Item2 }, 0, false, true);
                }
            }

            foreach (Identifiable.Id id in Identifiable.PLORT_CLASS)
            {
                if (Prefab.GetPrefab(id) != null)
                {
                    Prefab.GetPrefab(id).GetComponent<MeshFilter>().sharedMesh = (Mesh)Other.LoadAsset(typeof(Mesh), circle, "circle_plort");
                }
            }

            foreach (Identifiable.Id id in Identifiable.TOY_CLASS)
            {
                if (id == Identifiable.Id.ROBOT_TOY || id == Identifiable.Id.TREASURE_CHEST_TOY || id == Identifiable.Id.BOP_GOBLIN_TOY)
                    continue;
                if (Prefab.GetPrefab(id) != null)
                {
                    Prefab.GetPrefab(id).GetComponentInChildren<MeshFilter>().sharedMesh = (Mesh)Other.LoadAsset(typeof(Mesh), circle, "circle");
                }
            }

            foreach (Identifiable.Id id in Identifiable.STANDARD_CRATE_CLASS)
            {
                if (Prefab.GetPrefab(id) != null)
                    Prefab.GetPrefab(id).GetComponentInChildren<MeshFilter>().sharedMesh = (Mesh)Other.LoadAsset(typeof(Mesh), circle, "circle");
            }

            foreach (Identifiable.Id id in Identifiable.CRAFT_CLASS)
            {
                if (Prefab.GetPrefab(id) != null)
                    Prefab.GetPrefab(id).GetComponentInChildren<MeshFilter>().sharedMesh = (Mesh)Other.LoadAsset(typeof(Mesh), circle, "circle");
            }
        }
    }

    public class Main : ModEntryPoint
    {
        // Called before GameContext.Awake
        // You want to register new things and enum values here, as well as do all your harmony patching
        public override void PreLoad()
        {
            HarmonyInstance.PatchAll();
        }


        // Called before GameContext.Start
        // Used for registering things that require a loaded gamecontext
        public override void Load()
        {

        }

        // Called after all mods Load's have been called
        // Used for editing existing assets in the game, not a registry step
        public override void PostLoad()
        {

        }

    }
}