using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using HomeTask_1;
using HomeTask_1.Enums;
using HomeTask_1.EventArgs;
using NUnit.Framework;

namespace HomeTask_2
{
    public class FileSystemVisitorTests
    {
        private const string TestFolderPath = "test";
        private const int FileFolderCount = 5;
        private const int FolderCount = 2;
        private const string FileFolderFilter = "test";
        private const string FoundFileName = "test1.txt";
        private const string NotFoundFileName = "photo1_1.bmp";
        private FileSystemVisitor fileSystemVisitor;

        [Test]
        public void GetFileSystemInfos_TestFolderExistAndHaveFileInfos_CountSameAsExpected()
        {
            fileSystemVisitor = new FileSystemVisitor(TestFolderPath);

            var fileSystemInfos = fileSystemVisitor.GetFileSystemInfos();

            fileSystemInfos.Should().HaveCount(FileFolderCount);
        }

        [Test]
        public void GetFileSystemInfos_TestFolderExistAndHaveFilterFileInfos_CountSameAsExpected()
        {
            fileSystemVisitor = new FileSystemVisitor(TestFolderPath, info => info.Name.Contains("."));

            var fileSystemInfos = fileSystemVisitor.GetFileSystemInfos();

            fileSystemInfos.Should().HaveCount(FolderCount);
        }
        
        [Test]
        public void GetFileSystemInfos_TestFolderExistFilterFileFound_FilesNotContains()
        {
            fileSystemVisitor = new FileSystemVisitor(TestFolderPath, info => info.Name.Contains(FileFolderFilter));

            var fileSystemInfos = fileSystemVisitor.GetFileSystemInfos();

            fileSystemInfos.Should().NotContain(FileFolderFilter);
        }

        [Test]
        public void GetFileSystemInfos_TestFolderExistAndFileFound_EventArgsHasFoundFile()
        {
            ItemFoundEventArgs<FileInfo> eventArgs = null;
            fileSystemVisitor = new FileSystemVisitor(TestFolderPath, info => info.Name.Contains(FoundFileName));
            fileSystemVisitor.FilteredFileFound += (_, arg) =>
            {
                eventArgs = arg;
            };

            fileSystemVisitor.GetFileSystemInfos().ToList();

            eventArgs.FoundItem.Name.Should().BeEquivalentTo(FoundFileName);
        }
        
        [Test]
        public void GetFileSystemInfos_TestFolderExistAndFolderFound_EventArgsHasFoundFolder()
        {
            var name = "test1";
            ItemFoundEventArgs<DirectoryInfo> eventArgs = null;

            fileSystemVisitor = new FileSystemVisitor(TestFolderPath, info => info.Name.Contains(name));
            fileSystemVisitor.FilteredDirectoryFound += (_, arg) => eventArgs = arg;
            fileSystemVisitor.GetFileSystemInfos().ToList();

            eventArgs.FoundItem.Name.Should().BeEquivalentTo(name);
        }
        
        [Test]
        public void GetFileSystemInfos_TestFolderExistAndFolderFound_StopSearch()
        {
            fileSystemVisitor = new FileSystemVisitor(TestFolderPath, info => info.Name.Contains(FoundFileName));
            fileSystemVisitor.FilteredFileFound += (_, eventArgs) => eventArgs.Action = BehaviorAction.Stop;

            var result = fileSystemVisitor.GetFileSystemInfos().ToList();

            result.Should().NotContain(x=>x.Name == NotFoundFileName);
        }
        
        [Test]
        public void GetFileSystemInfos_TestFolderExist_StartEventEmit()
        {
            int startCount = 0;
            fileSystemVisitor = new FileSystemVisitor(TestFolderPath);
            fileSystemVisitor.Start += (_,_) => startCount++;

           fileSystemVisitor.GetFileSystemInfos().ToList();
           fileSystemVisitor.GetFileSystemInfos().ToList();
           
           startCount.Should().Be(2);
        }
        
        [Test]
        public void GetFileSystemInfos_TestFolderExist_StopEventEmit()
        {
            int finishCount = 0;
            fileSystemVisitor = new FileSystemVisitor(TestFolderPath);
            fileSystemVisitor.Finish += (_,_) => finishCount++;

            fileSystemVisitor.GetFileSystemInfos().ToList();
            fileSystemVisitor.GetFileSystemInfos().ToList();
           
            finishCount.Should().Be(2);
        }
    }
}