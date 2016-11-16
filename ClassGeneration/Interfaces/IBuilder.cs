using ClassGeneration.Properties;

namespace ClassGeneration.Interfaces
{
    public interface IBuilder<in TIn, out TOut>
    {
        TOut Build(TIn obj, Settings settings = null);
    }
}