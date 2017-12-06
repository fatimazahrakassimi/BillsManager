using SQLite;

namespace BillsManager.service
{
    public interface IFactureService
    {
        SQLiteConnection GetConnection(string databaseName);
        long GetSize(string databaseName);

    }
}
