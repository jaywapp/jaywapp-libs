using Jaywapp.Test.PropertyTools.TreeListBox.Model;
using PropertyTools;
using ReactiveUI;
using System.Collections.Generic;

namespace Jaywapp.Test.PropertyTools.TreeListBox.Item
{
    public class FileMetaDataItem : ReactiveObject, IDragSource, IDropTarget
    {
        #region Internal Field
        private bool _isExpanded;
        private bool _isSelected;
        #endregion

        #region Properties
        public ChangelistItem Parent { get; internal set; }
        
        public FileMetaData File { get; }

        public bool IsExpanded
        {
            get => _isExpanded;
            set => this.RaiseAndSetIfChanged(ref _isExpanded, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => this.RaiseAndSetIfChanged(ref _isSelected, value);
        }

        public bool IsDraggable => true;
        #endregion

        #region Constructor
        public FileMetaDataItem(ChangelistItem changelistItem, FileMetaData file)
        {
            Parent = changelistItem;
            File = file;
        }
        #endregion

        #region Functions
        /// <summary>
        /// <paramref name="node"/>을 해당 객체에 Drop할 수 있는지 여부
        /// </summary>
        /// <param name="node"></param>
        /// <param name="dropPosition"></param>
        /// <param name="effect"></param>
        /// <returns></returns>
        public bool CanDrop(IDragSource node, DropPosition dropPosition, DragDropEffect effect)
        {
            return Parent.CanDrop(node, dropPosition, effect);
        }

        /// <summary>
        /// Drag 되어질 때
        /// </summary>
        public void Detach()
        {
            Parent.Files.Remove(this);
            Parent.AfterDrop();
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
            Parent.Drop(items, dropPosition, effect, initialKeyStates);
        }

        /// <inheritdoc/>
        public override string ToString() =>$"{File.Path}";
        #endregion
    }
}
