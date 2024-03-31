namespace UserDataManager.Services.Interface
{
    public interface IReadDataServices<U>
    {
        public Task<IEnumerable<U>> ReadAllData();
        public Task<U> ReadData(int id);
    }
}
