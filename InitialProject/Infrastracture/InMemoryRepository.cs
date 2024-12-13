using System;
using InitialProject.core;

namespace InitialProject.Infrastracture
{
    internal class InMemoryRepository : Repository
    {
        private IDictionary<string, TryingClass> _objectCache;

        public InMemoryRepository()
        {
            _objectCache = new Dictionary<string, TryingClass>();
        }

        /**
         * A method for inserting data for a dictionary repository.
         */
        public void update(TryingClass value)
        {
            Console.WriteLine("PUT");
            _objectCache.Add(value.ID, value);
        }

        /**
         * A method for returning the data from the dictionary with the given key.
         */
        public TryingClass get(string key)
        {
            Console.WriteLine("GET");
            return _objectCache[key];
        }

        /*
         * A method for deleting data from the dictionary with the given key.
         */
        public void delete(string key)
        {
            Console.WriteLine("DELETE");
            _objectCache.Remove(key);
        }
    }
}