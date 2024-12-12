using System;

namespace InitialProject
{

	public class TryingClass(string FirstName, string SecondName)
    {
        public string FirstName { get; set; } = FirstName;
        public string LastName { get; set; } = SecondName;

        public override string ToString() 
		{
			return $"FirstName={FirstName}, LastName={LastName}";
		}
	}
}
