using System.Collections.Generic;
using System.Linq;

namespace Jaywapp.Test.PropertyTools.TreeListBox.Model
{
    public class Changelist
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IList<FileMetaData> Files { get; set; }

        public Changelist()
        {
        }

        public Changelist(int id, string description, IEnumerable<FileMetaData> files)
        {
            Id = id;
            Description = description;
            Files = files.ToList();
        }
    }
}
