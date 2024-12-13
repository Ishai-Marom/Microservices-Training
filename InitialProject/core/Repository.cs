namespace InitialProject.core
{
	/**
	* A repository interface that is meant to save <see cref="SomeDataEntity"/> in the database.
	*/
	internal interface IRepository
	{
		/**
		 * A method for create/update data for the repository.
		 */
		void Update(SomeDataEntity value);

		/**
		 * A method for returning the data from the repository.
		 */
		SomeDataEntity Get(string key);

		/*
		 * A method for deleting data from the repository.
		 */
		void Delete(string key);
	}
}