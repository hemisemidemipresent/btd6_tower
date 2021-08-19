using MelonLoader;
using UnityEngine;
using Assets.Scripts.Utils;
using BTD_Mod_Helper.Extensions;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers;
using Il2CppSystem;
using Assets.Scripts.Unity;
using System.IO;
using Assets.Scripts.Models.Towers.Weapons;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace slons
{

    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Input.GetKeyDown(KeyCode.F1))
            {
                log("saving towers");
                foreach (TowerModel towerModel in Game.instance.model.towers)
                {
                    removeCircularReferences(towerModel);
                    String path = "Towers\\" + towerModel.baseId + "\\" + towerModel.name + ".json";
                    if (File.Exists(path)) File.Delete(path);
                    FileIOUtil.SaveObject(path, towerModel);

                }
                log("finished");
            }
        }
        void removeCircularReferences(TowerModel tower)
        {
            if (tower.baseId == "Alchemist" && tower.tiers[0] >= 2)
            {
                foreach (WeaponModel wModel in tower.GetWeapons())
                {
                    AddAcidicMixtureToProjectileModel aa = wModel.projectile.GetBehavior<AddAcidicMixtureToProjectileModel>();
                    try { aa.Mutator.acidModel = null; }
                    catch (System.NullReferenceException e) { }

                }
                if (tower.tiers[0] == 5)
                {
                    LoadAlchemistBrewInfoModel l = tower.GetBehavior<LoadAlchemistBrewInfoModel>();
                    l.addAcidicMixtureToProjectileModel.Mutator.acidModel = null;
                }
            }
            else if (tower.baseId == "Benjamin" && tower.tier >= 7)
            {
                foreach (WeaponModel wModel in tower.GetWeapons())
                {
                    StripChildrenModel strip = wModel.projectile.GetBehavior<StripChildrenModel>();
                    strip.Mutator.stripChildrenModel = null;
                }
            }
        }
        public static void log(Il2CppSystem.Object obj, string name)
        {
            FileIOUtil.LogToFile(name + ".json", obj);
        }
        public static void log(string s)
        {
            MelonLogger.Msg(s);
        }
        public static void log(bool b)
        {
            MelonLogger.Log(b.ToString());
        }
    }
}
