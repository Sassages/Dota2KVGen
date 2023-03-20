using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_Script_Maker.KV_Parser
{
    public class KVBlock : KVItem
    {
        public List<KVItem> children;
        public string name;

        public KVBlock(string name)
        {
            this.name = name;
            children = new List<KVItem>();
        }

        //Copy constructor
        public KVBlock(KVBlock b)
        {
            this.name = b.name;
            children = new List<KVItem>();
            foreach (KVItem child in b.children)
            {
                if(child is KVStatement)
                {
                    KVStatement s = (KVStatement)child;
                    AppendItem(new KVStatement(s));
                }
                else if (child is KVBlock)
                {
                    KVBlock block = (KVBlock)child;
                    AppendItem(new KVBlock(block));
                }
            }
        }

        public void AppendItem(KVItem item)
        {
            item.parent = this;

            if(children.IndexOf(item) < 0)
                children.Add(item);
        }

        public override string ToString()
        {
            return GetIndentedString(0);
        }

        public override string GetIndentedString(int tabs)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(IndentString("\"" + name + "\"", tabs));

            sb.AppendLine(IndentString("{", tabs));
            tabs++;

            foreach(KVItem child in children)
            {
                KVStatement s = child as KVStatement;
                if (s != null)
                {
                    if (!s.hide)
                        sb.AppendLine(child.GetIndentedString(tabs));
                }
                else
                    sb.AppendLine(child.GetIndentedString(tabs));
            }

            tabs--;
            sb.AppendLine(IndentString("}", tabs));

            return sb.ToString();
        }

        public bool RemoveChildBlock(string BlockPath)
        {
            KVBlock BlockToRemove = GetChildBlock(BlockPath);
            if (BlockToRemove == null)
                return false;
            KVBlock ParentBlock = BlockToRemove.parent;
            if (ParentBlock == null)
                return false;

            return ParentBlock.children.Remove(BlockToRemove);
        }

        public bool RemoveChildStatement(string key)
        {
            for(int i = 0; i < children.Count; i++)
            {
                if(children[i] is KVStatement)
                {
                    KVStatement s = (KVStatement)children[i];
                    if (s.left == key)
                    {
                        children.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

        public string GetValue(string key, string BlockPath = null)
        {
            if (BlockPath == null)
            {
                return RecursiveSearchInBlock(key);
            }
            else
            {
                KVBlock b = GetChildBlock(BlockPath);

                if (b == null)
                    return null;

                foreach (KVItem child in b.children)
                {
                    if (child is KVStatement)
                    {
                        KVStatement s = (KVStatement)child;
                        if (s.left == key)
                            return s.right;
                    }
                }
                return null;
            }
        }

        private string RecursiveSearchInBlock(string key)
        {
            foreach (KVItem child in children)
            {
                if (child is KVStatement)
                {
                    KVStatement s = (KVStatement)child;
                    if (s.left == key)
                        return s.right;
                }

                else if (child is KVBlock)
                {
                    KVBlock b = (KVBlock)child;
                    return b.RecursiveSearchInBlock(key);
                }
            }
            return null;
        }


        //Specify a blockpath to speed up the search process. Make sure all the keys are in that path if you do.
        //Path format is "a/b/c/d" e.g. "Creature/" or "Creature" or "Creature/Wearables"
        //A path of "" is this block only.
        //A path of null means search everywhere.
        //Elements in the output array will be null if not found. The array object will be null if the specified block is not found.
        public string[] GetValues(string[] keys, string BlockPath = null)
        {
            string[] output = new string[keys.Length];
            if (BlockPath == null)
            {
                RecursiveSearchInBlock(keys, output);
                return output;
            }
            else
            {
                KVBlock b = GetChildBlock(BlockPath);

                if (b == null)
                    return null;

                foreach (KVItem child in b.children)
                {
                    if (child is KVStatement)
                    {
                        KVStatement s = (KVStatement)child;
                        int index = Array.IndexOf(keys, s.left);
                        if (index >= 0)
                            output[index] = s.right;
                    }
                }
                return output;
            }
        }

        private void RecursiveSearchInBlock(string[] keys, string[] values)
        {
            foreach (KVItem child in children)
            {
                if (child is KVStatement)
                {
                    KVStatement s = (KVStatement)child;
                    int index = Array.IndexOf(keys, s.left);
                    if (index >= 0)
                        values[index] = s.right;
                }

                else if (child is KVBlock)
                {
                    KVBlock b = (KVBlock)child;
                    b.RecursiveSearchInBlock(keys, values);
                }
            }
        }

        /// <summary>
        /// In cases where a single key is used more than once (e.g. "ItemDef" for cosmetics), we need to take in a single key 
        /// and return all the values that match. Does not take in a blockpath since these keys generally exist across multiple
        /// blocks.
        /// </summary>
        public string[] GetValuesMultiple(string key)
        {
            List<string> output = new List<string>();
            RecursiveSearchInBlock(key, output);
            return output.ToArray();
        }


        private void RecursiveSearchInBlock(string key, List<string> output)
        {
            foreach (KVItem child in children)
            {
                if (child is KVStatement)
                {
                    KVStatement s = (KVStatement)child;
                    if (s.left == key)
                        output.Add(s.right);
                }

                else if (child is KVBlock)
                {
                    KVBlock b = (KVBlock)child;
                    b.RecursiveSearchInBlock(key, output);
                }
            }
        }

        public bool SetValue(string key, string value, string BlockPath, bool CreateNew = true)
        {
            KVBlock b = GetChildBlock(BlockPath);
            if (BlockPath == null)
                return false;

            //If we can find the block, fill in values.
            if (b != null)
            {
                foreach (KVItem child in b.children)
                {
                    if (child is KVStatement)
                    {
                        KVStatement s = (KVStatement)child;
                        if (s.left == key)
                        {
                            s.right = value;
                            return true;
                        }
                    }
                }

                if (CreateNew)
                {
                    b.AppendItem(new KVStatement(key, value));
                    return true;
                }

            }
            //If we can't find the block, and CreateNew is true, create the block.
            else if (CreateNew)
            {
                b = CreateNewBlockDeep(BlockPath);
                b.AppendItem(new KVStatement(key, value));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets values with corresponding keys. E.g. if keys = "Health","Damage" and vals = "300","25", then hp is set to 300 and damage to 25.
        /// Only sets the values within the blockpath you specified. </summary>
        /// <param name="keys">The list of keys to set.</param>
        /// <param name="values">The list of values to set the corresponding keys to.</param>
        /// <param name="BlockPath">The path to where the keys should be found/placed.NOT OPTIONAL - I need to know where to insert new KVStatements. Empty string is this block only.</param>
        /// <param name="CreateNew">If true, creates new KVStatements where necessary. Will also create the block if necessary at the path specified.</param>
        /// <param name="RemoveIfEmpty">If true, then values that are null or empty remove the KV statement from the list.</param>
        public bool SetValues(string[] keys, string[] values, string BlockPath, bool CreateNew = true, bool RemoveIfEmpty = false)
        {
            KVBlock b = GetChildBlock(BlockPath);
            if (BlockPath == null)
                return false;

            if (keys.Length != values.Length)
                return false;

            //If we can find the block, fill in values.
            if(b != null)
            {
                bool[] CompletedValues = new bool[keys.Length];

                foreach (KVItem child in b.children)
                {
                    if (child is KVStatement)
                    {
                        KVStatement s = (KVStatement)child;
                        int index = Array.IndexOf(keys, s.left);
                        if (index >= 0)
                        {
                            s.right = values[index];
                            CompletedValues[index] = true;

                            if (RemoveIfEmpty && (values[index] == null || values[index] == ""))
                                s.hide = true;
                            else
                                s.hide = false;
                        }
                    }
                }

                if (CreateNew)
                {
                    for (int i = 0; i < CompletedValues.Length; i++)
                    {
                        if (!CompletedValues[i])
                        {
                            KVStatement s = new KVStatement(keys[i], values[i]);
                            b.AppendItem(s);

                            if (RemoveIfEmpty && (values[i] == null || values[i] == ""))
                                s.hide = true;
                            else
                                s.hide = false;
                        }
                    }
                }

            }
            //If we can't find the block, and CreateNew is true, create the block.
            else if (CreateNew)
            {
                b = CreateNewBlockDeep(BlockPath);
                for(int i = 0; i < keys.Length; i++)
                {
                    KVStatement s = new KVStatement(keys[i], values[i]);
                    b.AppendItem(s);

                    if (RemoveIfEmpty && (values[i] == null || values[i] == ""))
                        s.hide = true;
                    else
                        s.hide = false;
                }
            }
            return true;
        }

        /*
        Recursive method to create a new block
        Say this block has "b1/b2/" already existing, and path = "b1/b2/n1/n2/", then the algorithm should create blocks n1 with n2 nested inside. N2 is returned
        */
        private KVBlock CreateNewBlockDeep(string path)
        {
            int SubstringEnd = path.IndexOf('/');
            string BlockToFind;

            //If this is simply a single string with no '/'
            if (SubstringEnd == -1)
                BlockToFind = path;
            else
                BlockToFind = path.Substring(0, SubstringEnd);

            KVBlock b = GetChildBlock(BlockToFind);

            if (b == this)
                return this;

            //If this inner block exists, then pass the creation instruction to it instead.
            if(b != null)
            {
                return b.CreateNewBlockDeep(path.Substring(SubstringEnd + 1));
            }
            //If it doesn't, we need to start the creation process.
            else
            {
                b = new KVBlock(BlockToFind);
                AppendItem(b);
                if (SubstringEnd == -1)
                    return b;
                else
                    return b.CreateNewBlockDeep(path.Substring(SubstringEnd + 1));
            }

            return null; //Program should never reach here.
        }

        public KVBlock GetChildBlock(string path)
        {
            if (path == null)
                return null;

            if (path.Length == 0)
                return this;

            int SubstringEnd = path.IndexOf('/');

            string BlockToFind;

            //If this is simply a single string with no '/'
            if (SubstringEnd == -1)
                BlockToFind = path;
            else
                BlockToFind = path.Substring(0, SubstringEnd);

            foreach(KVItem child in children)
            {
                if(child is KVBlock)
                {
                    KVBlock block = (KVBlock)child;
                    if(block.name.Equals(BlockToFind))
                    {
                        if(SubstringEnd + 1 == path.Length || SubstringEnd == -1)
                            return block.GetChildBlock("");
                        else
                            return block.GetChildBlock(path.Substring(SubstringEnd + 1));
                    }
                }
            }

            return null; //Not found.
        }
    }
}
