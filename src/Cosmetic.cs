using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_Script_Maker
{
    public class Cosmetic
    {
        public string name;
        public int id;

        public string hero;

        public Cosmetic(){ }

        public Cosmetic(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public Cosmetic(string name, int id, string hero)
        {
            this.name = name;
            this.id = id;
            this.hero = hero;
        }

        /*
        Expected in the following format:

        "1"
		{
			"name"		"Anti-Mage's Glaive"
			"prefab"		"default_item"
			"image_inventory"		"econ/heroes/antimage/antimage_weapon"
            ...
        }
        */
        public static Cosmetic ParseFromSourceCosmeticBlock(string[] lines)
        {
            Cosmetic c = new Cosmetic();

            //Get ID
            string IDLine = lines[0];
            IDLine = IDLine.Replace("\t", "");
            IDLine = IDLine.Replace("\"", "");
            c.id = Int32.Parse(IDLine);

            //Get Name
            int CurrentLine = 1;
            while(!lines[CurrentLine].Contains("\"name\""))
            {
                CurrentLine++;
            }
            string NameLine = lines[CurrentLine];
            NameLine = NameLine.Replace("\"", "");
            NameLine = NameLine.Replace("name", "");
            NameLine = NameLine.Replace("\t", "");
            c.name = NameLine;
            
            //Make sure it's a wearable item, and not a cursor pack or something
            while (!lines[CurrentLine].Contains("\"prefab\""))
            {
                CurrentLine++;
            }
            string PrefabLine = lines[CurrentLine];
            PrefabLine = PrefabLine.Replace("\"", "");
            PrefabLine = PrefabLine.Replace("prefab", "");
            PrefabLine = PrefabLine.Replace("\t", "");
            if(PrefabLine != "wearable" && PrefabLine != "default_item")
            {
                return null;
            }

            //Get hero
            while (!lines[CurrentLine].Contains("\"used_by_heroes\""))
            {
                CurrentLine++;
            }
            CurrentLine += 2; //Assuming only 1 hero per cosmetic.
            string HeroLine = lines[CurrentLine];
            HeroLine = HeroLine.Replace("\"", "");
            HeroLine = HeroLine.Replace("1", "");
            HeroLine = HeroLine.Replace("\t", "");
            c.hero = HeroLine;

            return c;
        }

        public string GetSummaryString()
        {
            return (id + "\t" + name + "\t" + hero);
        }

        public static Cosmetic ParseFromSummaryFile(string line)
        {
            string[] attrib = line.Split('\t');

            Cosmetic c = new Cosmetic();
            c.id = Int32.Parse(attrib[0]);
            c.name = attrib[1];
            c.hero = attrib[2];

            return c;
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Cosmetic))
                return false;

            Cosmetic c = (Cosmetic)obj;

            if (c.id == id)
                return true;
            return false;
        }
    }
}
