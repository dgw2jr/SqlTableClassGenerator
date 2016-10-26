using System.Windows.Forms;

namespace SQLTableClassGenerator.UI
{
    public interface ITreeViewPopulator
    {
        void Populate(TreeView tree);
    }
}