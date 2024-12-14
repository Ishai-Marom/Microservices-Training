using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebAPI.Models
{
    public class DataRepository
    {
        public static DataRepository Instance;

        public static DataRepository GetInstance() {
            if (Instance == null) {
                Instance = new DataRepository();
            }

            return Instance;
        }

        private IDictionary<string, Data> dictionary;

        public DataRepository() {
            this.dictionary = new Dictionary<string, Data>();
        }

        public void Add(string key, Data value) {
            this.dictionary[key] = value;
        }

        public Data Get(string key) 
        {
            return dictionary[key];
        }

        public void Remove(string key) 
        {
            dictionary.Remove(key);
        }

        public bool Contains(string key)
        {
            return dictionary.ContainsKey(key);
        }
    }
}