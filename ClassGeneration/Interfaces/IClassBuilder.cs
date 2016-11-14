using ClassGeneration.Properties;
using Microsoft.CodeAnalysis.Editing;

namespace ClassGeneration.Interfaces
{
    public interface IClassBuilder<T>
    {
        string Build(T obj, Settings settings);
    }
}