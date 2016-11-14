using System;
using System.Windows.Forms;

namespace SQLTableClassGenerator.Interfaces
{
    public interface ITreeNodeClassGenerator
    {
        string Generate(TreeNode node, Action preAction = null);
        string Generate(string name, string parentName);
    }
}