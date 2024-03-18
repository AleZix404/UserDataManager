namespace UserDataManager.Services.Interface
{
    public interface IReadDataServices<T>
    {
        public Task<IEnumerable<T>> ReadAllUserDataList();
        public Task<T> ReadUserData(int id);
    }
}
