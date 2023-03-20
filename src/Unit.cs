using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dota2_Script_Maker.KV_Parser;

namespace Dota2_Script_Maker
{
    public class Unit
    {
        public KVBlock kv_block;

        public string unit_path;

        /* The subdirectory within the game folder in which this unit file resides */
        public string sub_directory = "";

        public Unit(string unit_path)
        {
            this.unit_path = unit_path;
            kv_block = new KVBlock("New_Unit");
        }

        public Unit DeepCopy()
        {
            Unit u = new Unit(unit_path);
            u.kv_block = new KV_Parser.KVBlock(kv_block);
            u.sub_directory = sub_directory;
            return u;
        }

        public string[] GetUnitKeysForGUI()
        {
            return kv_block.GetValues(new string[] {
                    "Model",
                    "ModelScale",
                    "Level",
                    "HasInventory",
                    "SoundSet",
                    "ArmorPhysical",
                    "MagicalResistance",
                    "AttackCapabilities",
                    "AttackRange",
                    "AttackDamageType",
                    "AttackDamageMin",
                    "AttackDamageMax",
                    "AttackRate",
                    "BountyGoldMin",
                    "BountyGoldMax",
                    "StatusHealth",
                    "StatusHealthRegen",
                    "StatusMana",
                    "StatusManaRegen",
                    "Ability1",
                    "Ability2",
                    "Ability3",
                    "Ability4",
                    "Ability5",
                    "Ability6",
                    "BountyXP",
                    "MovementSpeed",
                    "MovementCapabilities",
                    "ConsideredHero",
                    "AttributePrimary",
                    "AttributeBaseStrength",
                    "AttributeStrengthGain",
                    "AttributeBaseIntelligence",
                    "AttributeIntelligenceGain",
                    "AttributeBaseAgility",
                    "AttributeAgilityGain",
                    "ProjectileModel",
                    "ProjectileSpeed"});
        }

        public override string ToString()
        {
            return kv_block.name;
        }
    }
}
