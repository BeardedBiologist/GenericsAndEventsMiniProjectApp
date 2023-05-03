using System;

// create an app that:
// takes a list
// outputs a CSV file

namespace GenericsAndEventsMiniProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PersonModel> people = new List<PersonModel>
            {
                new PersonModel { FirstName = "Tim", LastName = "Corey", EmailAddress = "tim@iamtimcorey.com"},
                new PersonModel { FirstName = "Sue", LastName = "Storm", EmailAddress = "sue@iamtimcorey.com" },
                new PersonModel { FirstName = "John", LastName = "Smith", EmailAddress = "john@iamtimcorey.com" }
             };

            List<CarModel> cars = new List<CarModel>
            {
                new CarModel { Manufacturer = "Toyota", Model = "Camry" },
                new CarModel { Manufacturer = "Toyota", Model = "Corola" },
                new CarModel { Manufacturer = "Ford", Model = "HeckMustang" }
             };


            DataAccess<PersonModel> peopleData = new DataAccess<PersonModel>();
            peopleData.BadEntryFound += PeopleData_BadEntryFound;
            peopleData.SaveToCSV(people, @"/Users/joshuajamesoconnor/Projects/GenericsAndEventsMiniProjectApp/GenericsAndEventsMiniProject/output/people.csv");
            Console.WriteLine("Saved people to CSV");
            Console.WriteLine();

            DataAccess<CarModel> carData = new DataAccess<CarModel>();
            carData.BadEntryFound += CarData_BadEntryFound;
            carData.SaveToCSV(cars, @"/Users/joshuajamesoconnor/Projects/GenericsAndEventsMiniProjectApp/GenericsAndEventsMiniProject/output/cars.csv");
            Console.WriteLine("Saved cars to CSV");
            Console.WriteLine();

            Console.ReadLine();
        }

        private static void CarData_BadEntryFound(object? sender, CarModel e)
        {
            Console.WriteLine($"Bad entry found for {e.Manufacturer} {e.Model}");
        }

        private static void PeopleData_BadEntryFound(object? sender, PersonModel e)
        {
            Console.WriteLine($"Bad entry found for { e.FirstName } { e.LastName }");
        }
    }
}