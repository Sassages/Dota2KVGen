using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class CustomCodeBlock
{
    public List<CustomCodeBlock> blocks;
    public CustomCodeBlock parent; //Im lazy af
    public List<string> CustomCode;
    string name;

    public CustomCodeBlock(string name)
    {
        this.name = name;
        blocks = new List<CustomCodeBlock>();
        CustomCode = new List<string>();
    }

    public void AppendBlock(CustomCodeBlock b)
    {
        blocks.Add(b);
        b.parent = this;

    }

    public string GetName()
    {
        return name;
    }

    public void AppendCustomCode(string code)
    {
        CustomCode.Add(code);
    }

    public void RemoveCustomCode(int index)
    {
        CustomCode.RemoveAt(index);
    }

    public void RemoveMostRecentCustomCode()
    {
        CustomCode.RemoveAt(CustomCode.Count - 1);
    }

    public string GetCustomCodeString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < CustomCode.Count; i++)
        {
            sb.Append(CustomCode[i]);
            sb.Append("\t");

            if (++i >= CustomCode.Count)
                break;

            sb.Append(CustomCode[i]);

            sb.Append(Environment.NewLine);
        }
        return sb.ToString();
    }

    public string GetNestedStringForFile(int TabOffset)
    {
        string tabs = "\t\t";
        int NumTabs = TabOffset;

        StringBuilder sb = new StringBuilder();

        AddTabs(TabOffset, sb);
        sb.Append(name);

        sb.Append(Environment.NewLine);
        AddTabs(TabOffset, sb);
        sb.Append("{");

        sb.Append(Environment.NewLine);
        for (int i = 0; i < CustomCode.Count; i++)
        {
            AddTabs(TabOffset + 1, sb);
            sb.Append(CustomCode[i]);
            sb.Append(tabs);

            if (++i >= CustomCode.Count)
                break;

            sb.Append(CustomCode[i]);

            sb.Append(Environment.NewLine);
        }

        foreach (CustomCodeBlock b in blocks)
            sb.Append(b.GetNestedStringForFile(TabOffset + 1));

        sb.Append(Environment.NewLine);
        AddTabs(TabOffset, sb);
        sb.Append("}");

        return sb.ToString();
    }

    private void AddTabs(int n, StringBuilder sb)
    {
        string tabs = "\t\t";

        for (int i = 0; i < n; i++)
            sb.Append(tabs);
    }

    public override string ToString()
    {
        StringBuilder output = new StringBuilder();
        CustomCodeBlock b = parent;
        while (b != null)
        {
            output.Append("  ");
            b = b.parent;
        }
        output.Append(name);
        return output.ToString();
    }
}
