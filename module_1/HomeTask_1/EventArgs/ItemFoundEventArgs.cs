using System.IO;
using HomeTask_1.Enums;

namespace HomeTask_1.EventArgs
{
    public class ItemFoundEventArgs<T> : System.EventArgs
        where T : FileSystemInfo
    {
        public T FoundItem { get; set; }
        public BehaviorAction Action { get; set; }
    }
}