using InitialProject.core;

namespace InitialProject.Infrastracture
{
    /*
    * An in-memory implementation of the IRepository interface. Inserts and removes data from the running process iteslf.
    */
    internal class InMemoryRepository : IRepository
    {
        private readonly IDictionary<string, SomeDataEntity> objectCache;

        public InMemoryRepository()
        {
            objectCache = new Dictionary<string, SomeDataEntity>();
        }

        public void Update(SomeDataEntity value)
        {
            objectCache.Add(value.ID, value);
        }

        public SomeDataEntity Get(string key)
        {
            return objectCache[key];
        }

        public void Delete(string key)
        {
            objectCache.Remove(key);
        }
    }
}