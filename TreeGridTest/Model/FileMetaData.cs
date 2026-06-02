using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaywapp.Test.PropertyTools.TreeListBox.Model
{
    public class FileMetaData
    {
        public string Path { get; set; }

        public FileMetaData()
        {
        }

        public FileMetaData(string path)
        {
            Path = path;
        }
    }
}
