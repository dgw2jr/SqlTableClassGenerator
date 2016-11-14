namespace ClassGeneration.Interfaces
{
    public interface IBuilder<in TIn, out TOut>
    {
        TOut Build(string databaseName, TIn obj);
    }
}