using System;
using InitialProject.core;

namespace InitialProject.Infrastracture
{
    public class InMemoryRepository : Repository
    {
        private IDictionary<String, Object> _objectCache;

        public InMemoryRepository()
        {
            _objectCache = new Dictionary<String, Object>();
        }

        /**
         * A method for inserting data for a dictionary repository.
         */
        public void put(string key, object value)
        {
            Console.WriteLine("PUT");
            _objectCache.Add(key, value);
        }

        /**
         * A method for returning the data from the dictionary with the given key.
         */
        public object get(string key)
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