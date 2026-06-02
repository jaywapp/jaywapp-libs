using Jaywapp.Test.PropertyTools.TreeListBox.Model;
using PropertyTools;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Jaywapp.Test.PropertyTools.TreeListBox.Item
{
    public class ChangelistItem : ReactiveObject, IDragSource, IDropTarget
    {
        #region Internal Field
        private bool _isExpanded;
        private bool _isSelected;
        private ObservableCollection<FileMetaDataItem> _files = new ObservableCollection<FileMetaDataItem>();
        #endregion

        #region Properties
        public Changelist Changelist { get; }

        public ObservableCollection<FileMetaDataItem> Files
        {
            get => _files;
            set => this.RaiseAndSetIfChanged(ref _files, value);    
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            set => this.RaiseAndSetIfChanged(ref  _isExpanded, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => this.RaiseAndSetIfChanged(ref _isSelected, value);
        }

        public bool IsDraggable => true;
        #endregion

        #region Constructor
        public ChangelistItem(Changelist changelist)
        {
            Changelist = changelist;
            Files = changelist.Files
                .Select(f => new FileMetaDataItem(this, f))
                .ToObservableCollection();
        }
        #endregion

        #region Functions
        /// <summary>
        /// Drag 되어질 때
        /// </summary>
        public void Detach() { }

        /// <summary>
        /// <paramref name="node"/>을 해당 객체에 Drop할 수 있는지 여부
        /// </summary>
        /// <param name="node"></param>
        /// <param name="dropPosition"></param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public bool CanDrop(IDragSource node, DropPosition dropPosition, DragDropEffect effect)
        {
            return node is FileMetaDataItem file && !Files.Contains(file);
        }

        /// <summary>
        /// <paramref name="node"/>을 해당 객체에 Drop할 때
        /// </summary>
        /// <param name="items"></param>
        /// <param name="dropPosition"></param>
        /// <param name="effect"></param>
        /// <param name="initialKeyStates"></param>
        public void Drop(IEnumerable<IDragSource> items, DropPosition dropPosition, DragDropEffect effect, DragDropKeyStates initialKeyStates)
        {
            foreach(var item in items)
            {
                if (!(item is FileMetaDataItem fileItem))
                    continue;

                Files.Add(fileItem);
                fileItem.Parent = this;
            }

            Files.Sort(f => f.ToString());
            IsExpanded = true;
            AfterDrop();
        }

        /// <summary>
        /// Drop 이후 후처리
        /// </summary>
        public void AfterDrop()
        {
            Changelist.Files = Files.Select(f => f.File).ToList();
        }

        /// <inheritdoc/>
        public override string ToString() => $"{Changelist.Id} {Changelist.Description}";
        #endregion
    }
}
