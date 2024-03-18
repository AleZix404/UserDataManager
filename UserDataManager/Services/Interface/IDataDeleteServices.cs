namespace UserDataManager.Services.Interface
{
    public interface IDataDeleteServices
    {
        public Task<bool> RemoveData(int id);
        public Task<bool> RemoveOtherData(int id);
        public Task<bool> ClearAllUserDataClient();
    }
}
