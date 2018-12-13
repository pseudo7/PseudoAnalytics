using System.IO;
using System.Text;

namespace PseudoAnalytics
{
    public class CSVParser
    {
        static string fileName;
        static CSVParser _instance;
        StringBuilder builder;

        public CSVParser()
        {
            fileName = System.DateTime.Today.ToString("dd-MM-yyy") + ".csv";
            if (!Directory.Exists(PSAConstants.CSV_EXPORT_PATH))
                Directory.CreateDirectory(PSAConstants.CSV_EXPORT_PATH);
            if (!File.Exists(Path.Combine(PSAConstants.CSV_EXPORT_PATH, fileName)) || IsDataEmpty)
                File.WriteAllText(Path.Combine(PSAConstants.CSV_EXPORT_PATH, fileName), string.Join(",", new string[] { "Key", "Module Name", "Module Hits" + System.Environment.NewLine }));
        }

        public static CSVParser Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CSVParser();
                return _instance;
            }
        }

        public void SaveDataToCSV(CSVData data)
        {
            builder = new StringBuilder();
            builder.Append(data).Append(System.Environment.NewLine);
            File.AppendAllText(Path.Combine(PSAConstants.CSV_EXPORT_PATH, fileName), builder.ToString());
        }

        public void SaveDataToCSV(CSVData[] data)
        {
            builder = new StringBuilder();
            foreach (var item in data) builder.Append(item).Append(System.Environment.NewLine);
            File.AppendAllText(Path.Combine(PSAConstants.CSV_EXPORT_PATH, fileName), builder.ToString());
        }

        public CSVData GetLastDataFromCSV()
        {
            if (IsDataEmpty)
                return new CSVData { key = "", moduleName = "", moduleHits = "" };
            string[] data = File.ReadAllLines(Path.Combine(PSAConstants.CSV_EXPORT_PATH, fileName));
            return new CSVData(data[data.Length - 1].Split(','));
        }

        public CSVData[] GetAllDataFromCSV()
        {
            if (IsDataEmpty)
                return new CSVData[] { };
            string[] data = File.ReadAllLines(Path.Combine(PSAConstants.CSV_EXPORT_PATH, fileName));
            CSVData[] allData = new CSVData[data.Length];
            for (int i = 0; i < data.Length; i++)
                allData[i] = new CSVData(data[i].Split(','));
            return allData;
        }

        public bool IsDataEmpty
        {
            get
            {
                using (var stream = File.OpenText(Path.Combine(PSAConstants.CSV_EXPORT_PATH, fileName)))
                {
                    string s = "";
                    while ((s = stream.ReadLine()) != null)
                        if (!string.IsNullOrEmpty(s.Trim()))
                            return false;
                    return true;
                }
            }
        }
    }

    public struct CSVData
    {
        public string key;
        public string moduleName;
        public string moduleHits;

        public CSVData(string key, string moduleName, string moduleHits)
        {
            this.key = key;
            this.moduleName = moduleName;
            this.moduleHits = moduleHits;
        }

        public CSVData(string[] data)
        {
            key = data[0];
            moduleName = data[1];
            moduleHits = data[2];
        }

        public override string ToString()
        {
            return string.Join(",", new string[] { key, moduleName, moduleHits });
        }
    }
}
