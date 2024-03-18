namespace UserDataManager.Services.Interface
{
    public interface IDataUpdateServices<T>
    {
        public Task<T> UpdateUserData(T DataUpdateDTO);
    }
}
