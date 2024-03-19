namespace UserDataManager.Services.Interface
{
    public interface IReadDataServices<U>
    {
        public Task<IEnumerable<U>> ReadAllUserDataList();
        public Task<U> ReadUserData(int id);
    }
}
