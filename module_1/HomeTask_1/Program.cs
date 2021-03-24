using System;
using System.Linq;

namespace HomeTask_1
{
    static class Program
    {
        static void Main(string[] args)
        {
            var fileVisitor = 
                new FileSystemVisitor(@"C:\Users\Mykyta_Sokurenko\Downloads", info => !info.Name.EndsWith(".pdf"));

            fileVisitor.Start += (s, e) =>
            {
                Console.WriteLine("Start");
            };
            
            fileVisitor.Finish += (s, e) =>
            {
                Console.WriteLine("Finish");
            };

            fileVisitor.FileFound += (s, e) =>
            {
                Console.WriteLine($"File: {e.FoundItem.Name} are found");
            };
            
            fileVisitor.FilteredFileFound += (s, e) =>
            {
                Console.WriteLine($"Filtered file: {e.FoundItem.Name} are found");
            };
            
            fileVisitor.DirectoryFound += (s, e) =>
            {
                Console.WriteLine($"Directory: {e.FoundItem.Name} are found");
            };
            
            fileVisitor.FilteredDirectoryFound += (s, e) =>
            {
                Console.WriteLine($"Filtered directory: {e.FoundItem.Name} are found");
            };

            fileVisitor.GetFileSystemInfos().ToList();
        }
    }
}