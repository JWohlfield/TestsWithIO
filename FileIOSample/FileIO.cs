using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace FileIOSample
{
    class FileIO
    {
        public static string GetFilePath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            folderPath += @"\KrustyKrab";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return folderPath;
        }

        public static string GetSalesFileName(string filePath)
        {
            string fileName = @"\SalesLog.txt";
            string completePath = filePath + fileName;

            if (!File.Exists(completePath))
            {
                File.Create(completePath).Close();
            }

            return fileName;
        }

        public static string GetProductFileName(string filePath)
        {
            string fileName = @"\ProductMaster.txt";
            string completePath = filePath + fileName;

            if (!File.Exists(completePath))
            {
                File.Create(completePath).Close();
            }

            return fileName;
        }

        public static string GetProductCSVName(string filePath)
        {
            string fileName = @"\Products.csv";
            string completePath = filePath + fileName;

            if (!File.Exists(completePath))
            {
                File.Create(completePath).Close();
            }

            return fileName;
        }

        private void OpenFolder(string folderPath)
        {

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }

        }



        public static void WriteToFile(string filePath, string input)
        {
            StreamReader reader;
            StreamWriter writer;
            try
            {
                //we try and open the file along the file path 
                //The reader or writer will throw a file not found exception 
                reader = new StreamReader(filePath);
                string fileOutput = reader.ReadToEnd();
                List<string> existingRecords = fileOutput.Split(',').ToList();


                if (!fileOutput.Contains(input))
                {
                    fileOutput += $", {input}";

                }
                else
                {
                    Console.WriteLine("That entry already exists, the file has not been changed");
                    Console.WriteLine(fileOutput);
                }

                reader.Close();

                writer = new StreamWriter(filePath);
                writer.Write(fileOutput);

                writer.Close();
            }
            //With File IO you always want to put a try catch around it 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public static void PrintFile(string filePath, string fileType)
        {
            StreamReader reader;
            try
            {
                //we try and open the file along the file path 
                //The reader or writer will throw a file not found exception 
                reader = new StreamReader(filePath);
                string fileOutput = reader.ReadToEnd();
                List<string> existingRecords = fileOutput.Split(',').ToList();

                Console.WriteLine($"Existing Records in the {fileType} file: ");
                PrintRecords(existingRecords);

                reader.Close();

            }
            //With File IO you almost always want to put a try catch around it 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }


        public static void PrintRecords(List<string> records)
        {
            foreach (string record in records)
            {
                Console.WriteLine(record.Trim());
            }
        }

        public static void SaveToCsv<T>(List<T> listData, string path)
        {
            var lines = new List<string>();
            IEnumerable<PropertyDescriptor> props = TypeDescriptor.GetProperties(typeof(T)).OfType<PropertyDescriptor>();
            var header = string.Join(",", props.ToList().Select(x => x.Name));
            lines.Add(header);
            var valueLines = listData.Select(row => string.Join(",", header.Split(',').Select(a => row.GetType().GetProperty(a).GetValue(row, null))));
            lines.AddRange(valueLines);
            File.WriteAllLines(path, lines.ToArray());
        }
    }
}
