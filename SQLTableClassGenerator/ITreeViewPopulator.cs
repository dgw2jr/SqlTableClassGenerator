using System.Windows.Forms;

namespace SQLTableClassGenerator
{
    public interface ITreeViewPopulator
    {
        void Populate(TreeView tree);
    }
}