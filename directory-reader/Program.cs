using System;
using System.IO;
using System.Threading.Tasks;

namespace directory_reader
{
    public class Program
    {
        public static void Main()
        {
            Task<string[]>[] tasks = new Task<string[]>[2];
            string docsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            tasks[0] = Task<string[]>.Factory.StartNew(() => Directory.GetDirectories(docsDirectory));
            tasks[1] = Task<string[]>.Factory.StartNew(() => Directory.GetFiles(docsDirectory));

            Task.Factory.ContinueWhenAll(tasks, completedTasks =>
            {
                Console.WriteLine($"{docsDirectory} contains: ");
                Console.WriteLine($"\t{tasks[0].Result.Length} subdirectories:");
                foreach (var dir in tasks[0].Result)
                {
                    Console.WriteLine("\t" + $" {dir}");
                }
                Console.WriteLine($"\t\t{tasks[1].Result.Length} files");
                foreach (var files in tasks[1].Result)
                {
                    Console.WriteLine("\t\t" + $" {files}");
                }
            });

            Console.ReadLine();
        }
    }
}
