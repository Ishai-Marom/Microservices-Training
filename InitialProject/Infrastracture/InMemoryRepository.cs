using InitialProject.core;

namespace InitialProject.Infrastracture
{
    internal class InMemoryRepository : IRepository
    {
        private readonly IDictionary<string, SomeDataEntity> objectCache;

        public InMemoryRepository()
        {
            objectCache = new Dictionary<string, SomeDataEntity>();
        }

        /**
         * A method for inserting data for a dictionary repository.
         */
        public void Update(SomeDataEntity value)
        {
            objectCache.Add(value.ID, value);
        }

        /**
         * A method for returning the data from the dictionary with the given key.
         */
        public SomeDataEntity Get(string key)
        {
            return objectCache[key];
        }

        /*
         * A method for deleting data from the dictionary with the given key.
         */
        public void Delete(string key)
        {
            objectCache.Remove(key);
        }
    }
}