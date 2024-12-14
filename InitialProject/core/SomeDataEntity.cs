namespace InitialProject.core
{

	internal class SomeDataEntity(string id, string FirstName, string LastName)
    {
		private readonly string id = id;

		public string ID {get {return id;}}
        public string FirstName { get; set; } = FirstName;
        public string LastName { get; set; } = LastName;

        public override string ToString() 
		{
			return $"id={id}, FirstName={FirstName}, LastName={LastName}";
		}
	}
}
