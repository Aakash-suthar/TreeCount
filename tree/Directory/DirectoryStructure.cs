using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace tree
{
    public static class DirectoryStructure
    {
        public static List<Directoryitem> GetLogicalDrives() {

           return Directory.GetLogicalDrives().Select(drive => new Directoryitem { Fullpath = drive, Type = DirectoryItemType.Drive }).ToList();
        }
        public static List<Directoryitem> GetDirectoryContents(string fullpath)
        {
            var items = new List<Directoryitem>();
            try
            {
                var dirs = Directory.GetDirectories(fullpath);
                if (dirs.Length > 0)
                {

                    items.AddRange(dirs.Select(dir=>new Directoryitem {Fullpath=dir,Type=DirectoryItemType.Folder }));
                }
            }
            catch { }
            try
            {
                var f = Directory.GetFiles(fullpath);
                if (f.Length > 0)
                {

                    items.AddRange(f.Select(fs=>new Directoryitem { Fullpath=fs,Type=DirectoryItemType.File}));
                }
            }
            catch { }
            return items;
        }

        public static string getfilefolder(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;

            }
            var normal = path.Replace('/', '\\');

            var lastindex = normal.LastIndexOf('\\');

            if (lastindex <= 0)
            {
                return path;
            }

            return path.Substring(lastindex + 1);
        } 
    }
}
