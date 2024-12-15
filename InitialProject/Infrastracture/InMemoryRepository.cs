using InitialProject.core;

namespace InitialProject.Infrastracture
{
    /*
    * An in-memory implementation of the IRepository interface. Inserts and removes data from the running process iteslf.
    */
    internal class InMemoryRepository : IRepository
    {
        private readonly IDictionary<string, Bus> objectCache;

        public InMemoryRepository()
        {
            objectCache = new Dictionary<string, Bus>();
        }

        public void Update(Bus value)
        {
            objectCache.Add(value.ID, value);
        }

        public Bus Get(string key)
        {
            return objectCache[key];
        }

        public void Delete(string key)
        {
            objectCache.Remove(key);
        }
    }
}