namespace Services.SaveLoad
{
    public interface ISaveLoadService
    {
        public T Load<T>(string saveId) where T : ISavable;
        public void Save<T>(T obj) where T : ISavable;
        public bool Exists(string saveId);
    }
}