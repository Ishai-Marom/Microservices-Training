namespace InitialProject.core
{

	internal class Bus(string id, string driverName, string color, int passengersCapacity)
    {
		private readonly string id = id;

		public string ID {get {return id;}}
        public string DriverName { get; set; } = driverName;
		public string Color { get; set; } = color;
		public int PassengersCapacity {get; set;} = passengersCapacity;

		public override string ToString()
		{
			return $"id={id}, DriverName={DriverName}, Color={Color}, PassengersCapacity={PassengersCapacity}";
		}
	}
}
