namespace SQLTableClassGenerator
{
    public interface IConnectionHandler
    {
        string Server { get; set; }

        string GetConnectionString();
        void SetConnection();
    }
}