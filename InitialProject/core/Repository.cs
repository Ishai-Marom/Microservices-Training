namespace InitialProject.core
{
	/**
	* A repository interface that is meant to save <see cref="Bus"/> in the database.
	*/
	internal interface IRepository
	{
		/**
		 * A method for create/update data for the repository.
		 */
		void Update(Bus value);

		/**
		 * A method for returning the data from the repository.
		 */
		Bus Get(string key);

		/*
		 * A method for deleting data from the repository.
		 */
		void Delete(string key);
	}
}