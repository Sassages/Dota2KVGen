using System;
using System.Collections.Generic;
using System.IO;
using Dota2_Script_Maker.KV_Parser;
using System.Windows.Forms;

namespace Dota2_Script_Maker
{
    public class Unit_Reader
    {
        CustomCodeBlock CurrentBlock;

        public List<Cosmetic> cosmetics;

        /* A list of all sub-directories corresponding to each unit file */
        public List<string> all_sub_directories;

        public List<Unit> ReadExistingUnits(string unit_path)
        {
            List<Unit> units = new List<Unit>();
            List<string> all_unit_files = new List<string>();
            all_sub_directories = new List<string>();

            string[] unit_files = Directory.GetFiles(unit_path);
            string[] sub_directories = Directory.GetDirectories(unit_path);

            for (int j = 0; j < unit_files.Length; j++)
            {
                all_unit_files.Add(unit_files[j]);
                all_sub_directories.Add(unit_path);
            }

            List<string[]> new_sub_directories = new List<string[]>();
            new_sub_directories.Add(sub_directories);
            int sub_directory_count = new_sub_directories.Count;

            for (int i = 0; i < sub_directory_count; i++)
            {
                string[] current_directories = new_sub_directories[i];

                for(int j = 0; j < current_directories.Length; j++)
                {
                    string[] new_files = Directory.GetFiles(current_directories[j]);

                    for (int k = 0; k < new_files.Length; k++)
                    {
                        all_unit_files.Add(new_files[k]);
                        all_sub_directories.Add(current_directories[j]);
                    }

                    string[] new_sub_directory = Directory.GetDirectories(current_directories[j]);

                    if (new_sub_directory != null)
                    {
                        new_sub_directories.Add(new_sub_directory);
                        sub_directory_count++;
                    }

                }
            }

            unit_files = new string[all_unit_files.Count];
            for(int i = 0; i < all_unit_files.Count; i++)
            {
                unit_files[i] = all_unit_files[i];
            }

            for (int i = 0; i < unit_files.Length; i++)
            {
                string text = File.ReadAllText(unit_files[i]);
                KVLexer lex = new KVLexer();
                List<Token> ts = lex.LexicalAnalysis(text);

                if(lex.errors.Count > 0)
                {
                    continue;
                }

                KVParser parser = new KVParser(ts);
                KVBlock b = parser.Parse();

                if (parser.Error != null)
                {
                    continue;
                }

                string s = b.ToString();
                Unit unit = new Unit(unit_files[i]);
                unit.sub_directory = all_sub_directories[i];
                unit.kv_block = b;
                units.Add(unit);

                if (b == null)
                {
                    Console.WriteLine(parser.Error);
                }
            }

            return units;
        }

        public bool reloadUnit(Unit u)
        {
            string text = File.ReadAllText(u.sub_directory + "\\" + u.kv_block.name + ".txt");
            KVLexer lex = new KVLexer();
            List<Token> ts = lex.LexicalAnalysis(text);

            if (lex.errors.Count > 0)
            {
                MessageBox.Show(lex.errors[0].GetErrorMessage(), "Parsing Results");
                return false;
            }

            KVParser parser = new KVParser(ts);
            KVBlock b = parser.Parse();

            if (parser.Error != null)
            {
                MessageBox.Show(parser.Error.GetErrorMessage(), "Parsing Results");
                return false;
            }

            string s = b.ToString();
            u.kv_block = b;
            return true;
        }
    }
}
