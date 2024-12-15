namespace SimpleWebAPI.Models
{
    public interface BusRepository
    {
        /**
		 * A method for create/update bus for the repository.
		 */
        void Update(Bus value);

        /**
		 * A method for returning the bus from the repository.
		 */
        Bus Get(string key);

        /*
		 * A method for deleting bus from the repository.
		 */
        void Delete(string key);

        /*
         * A method for checking bus existence in the repository.
         */
        public bool Contains(string key);
    }
}
