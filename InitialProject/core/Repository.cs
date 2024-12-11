using System;


namespace InitialProject.core
{
	public interface Repository
	{
		/**
		 * A method for inserting data for a dictionary repository.
		 */
		void put(string key, object value);

		/**
		 * A method for returning the data from the dictionary with the given key.
		 */
		object get(string key);

		/*
		 * A method for deleting data from the dictionary with the given key.
		 */
		void delete(string key);
	}
}