using System;
using InitialProject.core;
using InitialProject.Infrastracture;


namespace InitialProject
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");

            TryingClass2 a = new TryingClass2();
            a.first = "5";
            a.second = "bi";
            Console.WriteLine(a.first + a.second);

            Repository m = new InMemoryRepository();

            m.put("1", a);
            var v = m.get("1");
            m.delete("1");

            Console.WriteLine("The returned value is " + v);
        }
    }
}