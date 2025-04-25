using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace game
{
    public struct ResultLine
    {
        public int result {  get; set; }
        public string time_result {  get; set; }

        public ResultLine(ResultLine line)
        {
            result = line.result;
            time_result = line.time_result;
        }
    }
    public class FileFunctions
    {
        public void ReadResults(string file_name, List<ResultLine> results)
        {
            results.Clear();
            if (File.Exists(file_name) & string.IsNullOrWhiteSpace(File.ReadAllText(file_name)) == false)
            {
                using (StreamReader file = new StreamReader(file_name))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        string[] words = line.Split('|');
                        ResultLine resultLine = new ResultLine
                        {
                            time_result = words[0],
                            result = int.Parse(words[1])
                        };
                        results.Add(resultLine);
                    }
                }
            }
        }

        public void WriteResults(string file_name, List<ResultLine> results)
        {
            using (StreamWriter file = new StreamWriter(file_name, false, System.Text.Encoding.Default))
            {
                foreach (var result in results)
                {
                    file.WriteLine($"{result.time_result}|{result.result}");
                }
            }
        }

        public void AddNewBestResult(string file_name, List<ResultLine> results, int maybe_best_result)
        {
            ReadResults(file_name, results);
            if (results.Count == 0 || maybe_best_result > results.Max(r => r.result))
            {
                ResultLine newResult = new ResultLine
                {
                    result = maybe_best_result,
                    time_result = DateTime.Now.ToString()
                };
                results.Add(newResult);
            }
            results = results.OrderByDescending(r => r.result).ToList();

            WriteResults(file_name, results);
        }
        

        public void WriteTableResults(DataTable table, List<ResultLine> results)
        {
            results = results.OrderByDescending(r => r.result).ToList();
            table.Clear();
            foreach(ResultLine line in results)
            {
                table.Rows.Add(line.time_result, line.result);
            }
        }

    }
    public static class Program
    {

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
