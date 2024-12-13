using System;

namespace InitialProject
{

	internal class TryingClass(string id, string FirstName, string SecondName)
    {
		private readonly string id = id;

		public string ID {get {return id;}}
        public string FirstName { get; set; } = FirstName;
        public string LastName { get; set; } = SecondName;

        public override string ToString() 
		{
			return $"id={id}, FirstName={FirstName}, LastName={LastName}";
		}
	}
}
