using Jaywapp.Test.PropertyTools.TreeListBox.Item;
using Jaywapp.Test.PropertyTools.TreeListBox.Model;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Jaywapp.Test.PropertyTools.TreeListBox
{
    public class MainWindowViewModel : ReactiveObject
    {
        private ObservableCollection<ChangelistItem> _changelistItems;
        public List<Changelist> Changelists { get; }

        public ObservableCollection<ChangelistItem> ChangelistItems
        {
            get => _changelistItems;
            set => this.RaiseAndSetIfChanged(ref _changelistItems, value);
        }

        public MainWindowViewModel()
        {
            Changelists = new List<Changelist>();
            for (int i = 0; i < 10; i++)
            {
                var id = i + 1;
                var desc = $"Description{id}";
                var files = new List<FileMetaData>();

                for (int j = 0; j < 10; j++)
                    files.Add(new FileMetaData($"[{id}] path{j + 1}"));

                var changelist = new Changelist(id, desc, files);
                Changelists.Add(changelist);
            }

            ChangelistItems = Changelists.Select(cl => new ChangelistItem(cl)).ToObservableCollection();
        }
    }
}
