// create an app that:
// takes a list
// outputs a CSV file

namespace GenericsAndEventsMiniProject
{
    public class DataAccess<T> where T : new()
    {
        public event EventHandler<T> BadEntryFound;

        public void SaveToCSV(List<T> items, string filepath)
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
                bool badWordDetected = false;

                foreach (var col in cols)
                {
                    string val = col.GetValue(item, null).ToString();

                    badWordDetected = BadWordDetector(val);
                    if (BadWordDetector(val) == true)
                    {
                        BadEntryFound?.Invoke(this, item);
                        break;

                    }

                    row += $",{ val }";
                }


                if (badWordDetected == false)
                {
                    row = row.Substring(1);
                    rows.Add(row);
                }
            }

            File.WriteAllLines(filepath, rows);
        }

        private bool BadWordDetector(string stringToTest)
        {
            bool output = false;

            string lowerCaseTest = stringToTest.ToLower();
            if (lowerCaseTest.Contains("darn") || lowerCaseTest.Contains("heck"))
            {
                output = true;
            }

            return output;
        }
    }
}