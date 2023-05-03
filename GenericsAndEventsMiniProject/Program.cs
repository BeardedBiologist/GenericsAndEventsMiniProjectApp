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
                new CarModel { Manufacturer = "Ford", Model = "Mustang" }
             };

            people.SaveToCSV(@"/Users/joshuajamesoconnor/Projects/GenericsAndEventsMiniProjectApp/GenericsAndEventsMiniProject/people.csv");
            cars.SaveToCSV(@"/Users/joshuajamesoconnor/Projects/GenericsAndEventsMiniProjectApp/GenericsAndEventsMiniProject/cars.csv");


            Console.ReadLine();
        }
    }

    public static class DataAccess
    {
        public static void SaveToCSV<T>(this List<T> items, string filepath) where T: new()
        {
            List<string> rows = new List<string>();

            T entry = new T();
            var cols = entry.GetType().GetProperties();
            string row = "";

            foreach (var col in cols)
            {
                row += $",{col.Name}";
            }
            row = row.Substring(1);
            rows.Add(row);

            foreach (var item in items)
            {
                row = "";

                foreach (var col in cols)
                {
                    row += $",{ col.GetValue(item, null) }";
                }
                row = row.Substring(1);
                rows.Add(row);
            }

            File.WriteAllLines(filepath, rows);
        }
    }
}