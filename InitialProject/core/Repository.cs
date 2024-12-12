using System;


namespace InitialProject.core
{
	public interface Repository
	{
		/**
		 * A method for inserting data for a dictionary repository.
		 */
		void put(string key, TryingClass value);

		/**
		 * A method for returning the data from the dictionary with the given key.
		 */
		TryingClass get(string key);

		/*
		 * A method for deleting data from the dictionary with the given key.
		 */
		void delete(string key);
	}
}