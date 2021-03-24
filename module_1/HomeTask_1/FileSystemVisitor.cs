using System;
using System.Collections.Generic;
using System.IO;
using HomeTask_1.Enums;
using HomeTask_1.EventArgs;

namespace HomeTask_1
{
    public class FileSystemVisitor
    {
        private readonly DirectoryInfo rootDirectory;
        private readonly Func<FileSystemInfo, bool> filter;

        public FileSystemVisitor(
            string rootDirectoryPath,
            Func<FileSystemInfo, bool> filter = null)
        {
            rootDirectory = new DirectoryInfo(rootDirectoryPath);
            this.filter = filter;
        }

        public event EventHandler Start;
        public event EventHandler Finish;

        public event EventHandler<ItemFoundEventArgs<FileInfo>> FileFound;
        public event EventHandler<ItemFoundEventArgs<FileInfo>> FilteredFileFound;

        public event EventHandler<ItemFoundEventArgs<DirectoryInfo>> DirectoryFound;
        public event EventHandler<ItemFoundEventArgs<DirectoryInfo>> FilteredDirectoryFound;

        public IEnumerable<FileSystemInfo> GetFileSystemInfos()
        {
            Start?.Invoke(this, null!);

            foreach (var fileSystemInfo in GetFileSystemInfos(rootDirectory))
            {
                yield return fileSystemInfo;
            }

            Finish?.Invoke(this, null!);
        }

        private IEnumerable<FileSystemInfo> GetFileSystemInfos(
            DirectoryInfo directoryInfo)
        {
            var fileSystemInfos = directoryInfo.EnumerateFileSystemInfos();
            var currentAction = BehaviorAction.Continue;

            foreach (var fileSystemInfo in fileSystemInfos)
            {
                if (fileSystemInfo is FileInfo file)
                {
                    currentAction = Handle(file, FileFound, FilteredFileFound);
                }

                if (fileSystemInfo is DirectoryInfo directory)
                {
                    currentAction = Handle(directory, DirectoryFound, FilteredDirectoryFound);

                    if (currentAction == BehaviorAction.Continue)
                    {
                        yield return directory;

                        foreach (var innerInfo in GetFileSystemInfos(directory))
                        {
                            yield return innerInfo;
                        }

                        continue;
                    }
                }

                if (currentAction == BehaviorAction.Stop)
                {
                    yield break;
                }

                yield return fileSystemInfo;
            }
        }

        private BehaviorAction Handle<T>(
            T item,
            EventHandler<ItemFoundEventArgs<T>> onFound,
            EventHandler<ItemFoundEventArgs<T>> onFilteredFound)
            where T : FileSystemInfo
        {
            var fileEvent = GetItemFoundEventTemplate(item);

            onFound?.Invoke(this, fileEvent);

            if (fileEvent.Action != BehaviorAction.Continue || filter == null)
            {
                return fileEvent.Action;
            }

            if (filter(item))
            {
                fileEvent = GetItemFoundEventTemplate(item);

                onFilteredFound?.Invoke(this, fileEvent);

                return fileEvent.Action;
            }

            return BehaviorAction.Skip;
        }
        
        private ItemFoundEventArgs<T> GetItemFoundEventTemplate<T>(T itemInfo)
            where T : FileSystemInfo
        {
            var fileEvent = new ItemFoundEventArgs<T>
            {
                FoundItem = itemInfo
            };

            return fileEvent;
        }
    }
}