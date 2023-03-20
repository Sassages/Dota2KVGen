using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dota2_Script_Maker
{
    public class CosmeticsReader
    {
        public static void GenerateCosmeticsFile()
        {
            List<Cosmetic> cosmetics = new List<Cosmetic>(5000);
            string[] lines = File.ReadAllLines(@"C:\Users\User\Desktop\items_game.txt");

            int TopOfBlockIndex = 3800; //Anti Mage glaive.
            int BotOfBlockIndex = -1;

            while (TopOfBlockIndex < 319319) //Veno Tail (Tail of the Ferocious Toxicant)
            {
                bool BottomFound = false;
                int CurrentLine = TopOfBlockIndex;
                int OpenBracketCount = 0;
                while (!BottomFound)
                {
                    if (lines[CurrentLine].Contains("{"))
                        OpenBracketCount++;
                    if (lines[CurrentLine].Contains("}"))
                    {
                        OpenBracketCount--;
                        if (OpenBracketCount == 0)
                        {
                            BottomFound = true;
                            BotOfBlockIndex = CurrentLine;
                        }
                    }
                    CurrentLine++;
                }

                string[] CosmeticBlock = new string[BotOfBlockIndex - TopOfBlockIndex + 1];
                Array.Copy(lines, TopOfBlockIndex, CosmeticBlock, 0, CosmeticBlock.Length);
                Cosmetic c = Cosmetic.ParseFromSourceCosmeticBlock(CosmeticBlock);

                //Returns null if it's not an item a hero can use (e.g. cursor pack or ward skin).
                if(c != null)
                    cosmetics.Add(c);

                TopOfBlockIndex = BotOfBlockIndex + 1;
            }

            string[] output = new string[cosmetics.Count];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = cosmetics[i].GetSummaryString();
            }

            File.WriteAllLines(@"C:\Users\User\Desktop\CosmeticList.txt", output);
        }

        public static List<Cosmetic> ReadInSummaryFile()
        {
            string[] lines = File.ReadAllLines(System.IO.Directory.GetCurrentDirectory() + @"\CosmeticList.txt");

            List<Cosmetic> c = new List<Cosmetic>(5000);
            foreach(string line in lines)
            {
                c.Add(Cosmetic.ParseFromSummaryFile(line));
            }

            return c;
        }
    }
}
