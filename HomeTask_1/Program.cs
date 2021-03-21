using System;
using System.Linq;
using HomeTask_1.Enums;

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
            
            fileVisitor.FilteredFileFound += (s, e) =>
            {
                Console.WriteLine(e.FoundItem.Name + " are found");

                if (e.FoundItem.Name.EndsWith(".jpeg"))
                {
                    e.Action = BehaviorAction.Stop;
                }
            };
            
            var fileSystemInfos = fileVisitor.GetFileSystemInfos().ToList();
        }
    }
}