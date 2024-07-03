// Interface defining methods for data storage operations
namespace CarDataStorageManager.Interfaces
{
    public interface IDataStorage
    {
        void Read();
        public void Save(string xmlFilePath);
    }
}
